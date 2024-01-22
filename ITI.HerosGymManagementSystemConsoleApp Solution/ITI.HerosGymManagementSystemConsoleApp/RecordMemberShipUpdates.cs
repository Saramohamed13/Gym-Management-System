using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.HerosGymManagementSystemConsoleApp
{
    internal class RecordMemberShipUpdates
    {
        #region Fields
        private string? name;
        private int amount;
        private int period; 
        #endregion

        #region Properties
        public string? Name { get => name; set => name = value; }
        public int Amount { get => amount; set => amount = value; }
        public int Period { get => period; set => period = value; }
        #endregion

        #region Methods

        public RecordMemberShipUpdates(string? _name, int _amount, int _period)
        {
            name = _name;

            amount = _amount;
            period = _period;
        }

        #endregion
    }
}
