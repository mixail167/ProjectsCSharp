using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SAPIntegration
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //RfcConfigParameters rfcParams = new RfcConfigParameters();
            //rfcParams.Add(RfcConfigParameters.AppServerHost, "METdb");
            //rfcParams.Add(RfcConfigParameters.SystemNumber, "01");
            //rfcParams.Add(RfcConfigParameters.SystemID, "MET");
            //rfcParams.Add(RfcConfigParameters.User, "KOVALEV");
            //rfcParams.Add(RfcConfigParameters.Password, "m19n28j375k");
            //rfcParams.Add(RfcConfigParameters.Client, "112");
            //rfcParams.Add(RfcConfigParameters.Language, "RU");
            //rfcParams.Add(RfcConfigParameters.PoolSize, "10");
            //RfcDestination sapRfcDestination = RfcDestinationManager.GetDestination(rfcParams);
            RfcDestination sapRfcDestination = RfcDestinationManager.GetDestination("ProduktiveSystem");
            RfcRepository sapRfcRepository = sapRfcDestination.Repository;
            IRfcFunction bapiGetCompanyList = sapRfcRepository.CreateFunction("BAPI_COMPANY_GETLIST");
            IRfcFunction bapiGetCompanyDetail = sapRfcRepository.CreateFunction("BAPI_COMPANY_GETDETAIL");
            bapiGetCompanyList.Invoke(sapRfcDestination);
            IRfcTable companyTable = bapiGetCompanyList.GetTable("COMPANY_LIST");
            IRfcStructure rfcReturn = bapiGetCompanyList.GetStructure("RETURN");
            for (int i = 0; i < companyTable.RowCount; i++)
            {
                companyTable.CurrentIndex = i;
                string companyNumber = companyTable.GetString("COMPANY");
                string name1 = companyTable.GetString("NAME1");
                bapiGetCompanyDetail.SetValue("COMPANYID", companyNumber);
                bapiGetCompanyDetail.Invoke(sapRfcDestination);
                IRfcStructure companyDetail = bapiGetCompanyDetail.GetStructure("COMPANY_DETAIL");
                string name2 = companyDetail.GetString("NAME2");
                listView1.Items.Add(new ListViewItem(new string[] { name1, name2 }));
            }
        }
    }
}
