

namespace PBL2_BookStoreManagement.BUS
{
    class BUS_Customer
    {
        #region Singleton
        private static BUS_Customer instance;
        private BUS_Customer() { }
        public static BUS_Customer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BUS_Customer();
                }
                return instance;
            }
        }
        #endregion
        #region Customer Operations
        
        #endregion

    }
}
