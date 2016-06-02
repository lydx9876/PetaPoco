using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using PetaPoco.Core;
using PetaPoco.Internal;

namespace PetaPoco
{
    /// <summary>
    /// 对实现了 IMapper 接口的实体实例的管理
    /// </summary>
    public static class Mappers
    {
        private static Dictionary<object, IMapper> _mappers = new Dictionary<object, IMapper>();
        private static ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        /// <summary>
        /// 对指定程序集里的所有类型注册映射器
        /// </summary>
        /// <param name="assembly">要注册的程序集</param>
        /// <param name="mapper">实现了 IMapper 的映射器</param>
        public static void Register(Assembly assembly, IMapper mapper)
        {
            RegisterInternal(assembly, mapper);
        }

        /// <summary>
        /// 对单个实体类型注册映射器
        /// </summary>
        /// <param name="type">要注册的实体类型</param>
        /// <param name="mapper">实现了 IMapper 的映射器</param>
        public static void Register(Type type, IMapper mapper)
        {
            RegisterInternal(type, mapper);
        }

        /// <summary>
        /// 注销指定程序集里所有类型已注册的映射器
        /// </summary>
        /// <param name="assembly">要注销注册的程序集</param>
        public static void Revoke(Assembly assembly)
        {
            RevokeInternal(assembly);
        }

        /// <summary>
        /// 注销指定实体类型已注册的映射器
        /// </summary>
        /// <param name="type">要注销注册的实体类型</param>
        public static void Revoke(Type type)
        {
            RevokeInternal(type);
        }

        /// <summary>
        /// 注销指定的映射器
        /// </summary>
        /// <param name="mapper">要注销的映射器</param>
        public static void Revoke(IMapper mapper)
        {
            _lock.EnterWriteLock();
            try
            {
                foreach (var i in _mappers.Where(kvp => kvp.Value == mapper).ToList())
                    _mappers.Remove(i.Key);
            }
            finally
            {
                _lock.ExitWriteLock();
                FlushCaches();
            }
        }

        /// <summary>
        /// 注销所有映射器
        /// </summary>
        public static void RevokeAll()
        {
            _lock.EnterWriteLock();
            try
            {
                _mappers.Clear();
            }
            finally
            {
                _lock.ExitWriteLock();
                FlushCaches();
            }
        }

        /// <summary>
        ///     Retrieve the IMapper implementation to be used for a specified POCO type.
        /// </summary>
        /// <param name="entityType">The entity type to get the mapper for.</param>
        /// <param name="defaultMapper">The default mapper to use when non is registered for the type.</param>
        /// <returns>The mapper for the given type.</returns>
        public static IMapper GetMapper(Type entityType, IMapper defaultMapper)
        {
            _lock.EnterReadLock();
            try
            {
                IMapper val;
                if (_mappers.TryGetValue(entityType, out val))
                    return val;
                if (_mappers.TryGetValue(entityType.Assembly, out val))
                    return val;

                return defaultMapper;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        private static void RegisterInternal(object typeOrAssembly, IMapper mapper)
        {
            _lock.EnterWriteLock();
            try
            {
                _mappers.Add(typeOrAssembly, mapper);
            }
            finally
            {
                _lock.ExitWriteLock();
                FlushCaches();
            }
        }

        private static void RevokeInternal(object typeOrAssembly)
        {
            _lock.EnterWriteLock();
            try
            {
                _mappers.Remove(typeOrAssembly);
            }
            finally
            {
                _lock.ExitWriteLock();
                FlushCaches();
            }
        }

        private static void FlushCaches()
        {
            // Whenever a mapper is registered or revoked, we have to assume any generated code is no longer valid.
            // Since this should be a rare occurrence, the simplest approach is to simply dump everything and start over.
            MultiPocoFactory.FlushCaches();
            PocoData.FlushCaches();
        }
    }
}