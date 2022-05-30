using LuisAnrango.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TDD_LuisAnrango
{
    [TestClass]
    public class UnitTestBase
    {
        protected BaseContext CrearContext(string nombrebd)
        {
            var option = new DbContextOptionsBuilder<BaseContext>()
                .UseInMemoryDatabase(nombrebd).Options;

            var dbContext = new BaseContext(option);
            return dbContext;
        }
    }
}
