// <copyright company="PetaPoco - CollaboratingPlatypus">
//      Apache License, Version 2.0 https://github.com/CollaboratingPlatypus/PetaPoco/blob/master/LICENSE.txt
// </copyright>
// <author>PetaPoco - CollaboratingPlatypus</author>
// <date>2016/01/31</date>

using Xunit;

namespace PetaPoco.Tests.Integration.Databases.Firebird
{
    [Collection("FirebirdTests")]
    public class FirebirdUpdateTests : BaseUpdateTests
    {
        public FirebirdUpdateTests()
            : base(new FirebirdDBTestProvider())
        {
        }
    }
}