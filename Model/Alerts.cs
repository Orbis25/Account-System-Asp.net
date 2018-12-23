using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    #region Info
    /*
    Alerts.Type = 1 : Success Insert
    Alerts.Type = 2 : Error Insert
    Alerts.Type = 3 : Success Delete
    Alerts.Type = 4 : Error Delete
    Alerts.Type = 5 : Success Update
    ------------------------------------------
    Alerts.Type = 6 : Success CreateAccount
    Alerts.Type = 7 : Error UpdateAccount
    Alerts.Type = 8 : Success UpdateAccount
    Alerts.Type = 9 : Account Delete Success
    ------------------------------------------
    Alerts.Type = 10 : Debs Add Success
    Alerts.Type = 11 : Update success
    Alerts.Type = 12 : Success Delete
    Alerts.Type = 13 : Error add and error update 
    Alerts.Type = 14 : Pay success
    Alerts.Type = 15 : Limit pay 
    */
    #endregion

    public class Alerts
    {
        public static int Type = 0;
    }
}
