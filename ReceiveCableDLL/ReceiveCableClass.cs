/* Title:           Receive Cable Class
 * Date:            5-21-16
 * Author:          Terry Holmes
 *
 * Description:     This class is used to do all Receive Transactions */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewEventLogDLL;
using CableTransactionIDDLL;

namespace ReceiveCableDLL
{
    public class ReceiveCableClass
    {
        //setting up classes
        EventLogClass TheEventLogClass = new EventLogClass();
        CableTransactionIDClass TheCableTransactionIDClass = new CableTransactionIDClass();

        //setting up the data
        ReceiveCableDataSet aReceiveCableDataSet;
        ReceiveCableDataSet TheReceiveCableDataSet;
        ReceiveCableDataSetTableAdapters.receivecableTableAdapter aReceiveCableTableAdapter;

        //public method to create a new reel
        public bool ReceiveNewCableReel(int intPartID, int intWarehouseID, int intProjectID, int intFootage, string strReelID, string strMSR, DateTime datTransactionDate)
        {
            //setting local variables
            bool blnFatalError = false;

            try
            {
                //filling the data set
                TheReceiveCableDataSet = GetReceiveCableInfo();

                //creating new table row
                ReceiveCableDataSet.receivecableRow NewTableRow = TheReceiveCableDataSet.receivecable.NewreceivecableRow();

                //filling the fields
                NewTableRow.TransactionID = TheCableTransactionIDClass.CreateTransactionID();
                NewTableRow.PartID = intPartID;
                NewTableRow.WarehouseID = intWarehouseID;
                NewTableRow.ProjectID = intProjectID;
                NewTableRow.ReelID = strReelID;
                NewTableRow.MSR = strMSR;
                NewTableRow.Footage = intFootage;
                NewTableRow.Date = datTransactionDate;

                //updating the data set
                TheReceiveCableDataSet.receivecable.Rows.Add(NewTableRow);
                UpdateReceiveCableDB(TheReceiveCableDataSet);
            }
            catch (Exception Ex)
            {
                //creating event log certification
                TheEventLogClass.InsertEventLogEntry(DateTime.Now, "Receive Cable Class Receive New Cable Reel " + Ex.Message);
            }

            //returning value
            return blnFatalError;
        }    
        public ReceiveCableDataSet GetReceiveCableInfo()
        {
            try
            {
                //filling data set
                aReceiveCableDataSet = new ReceiveCableDataSet();
                aReceiveCableTableAdapter = new ReceiveCableDataSetTableAdapters.receivecableTableAdapter();
                aReceiveCableTableAdapter.Fill(aReceiveCableDataSet.receivecable);

            }
            catch (Exception Ex)
            {
                //creating event log certification
                TheEventLogClass.InsertEventLogEntry(DateTime.Now, "Receive Cable Class Get Receive Cable Info " + Ex.Message);
            }

            //returning value
            return aReceiveCableDataSet;
        }
        public void UpdateReceiveCableDB(ReceiveCableDataSet aReceiveCableDataSet)
        {
            try
            {
                //filling data set
                aReceiveCableTableAdapter = new ReceiveCableDataSetTableAdapters.receivecableTableAdapter();
                aReceiveCableTableAdapter.Update(aReceiveCableDataSet.receivecable);

            }
            catch (Exception Ex)
            {
                //creating event log certification
                TheEventLogClass.InsertEventLogEntry(DateTime.Now, "Receive Cable Class Update Cable Reel DB " + Ex.Message);
            }
        }
    }
}
