using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.Office.Server.Utilities;
using Microsoft.SharePoint;

namespace SpMaterialPresupuesto.VisualWebPart1
{
    public partial class VisualWebPart1UserControl : UserControl
    {
        DataTable dtable;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            dtable = new DataTable();

            dtable.Columns.Add("ID");
            dtable.Columns.Add("Title");
            dtable.Columns.Add("Peticion");
            dtable.Columns.Add("Realizado_x0020_por");
            dtable.Columns.Add("Fecha");
            dtable.Columns.Add("Importe", typeof(Decimal));
        }

        private void ProcessItem(SPListItem item)
        {
            string uniqueId = item["ID"].ToString();
            string title = item["Title"].ToString();
            string peticion = item["Peticion"].ToString();
            string realizada;
            try
            {
                realizada = item["Realizado_x0020_por"].ToString();
            }
            catch (Exception e)
            {
                realizada = "Fran";
            }
            string fecha;
            try
            {
                fecha = item["Fecha"].ToString();

            }
            catch (Exception e)
            {
                fecha = "01/01/2016";
            }
            decimal importe = Decimal.Parse(item["Importe"].ToString());
            dtable.Rows.Add(uniqueId, title, peticion, realizada, fecha, importe);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnPreRender(EventArgs e)
        {
            SPQuery query = new SPQuery();

            query.Query = @"
<Where>
    <Eq>
        <FieldRef Name=""Estado""></FieldRef>
        <Value Type=""Choice"">Pendiente</Value>
    </Eq>
</Where>
";

            query.ViewFields = @"
<FieldRef Name=""ID""/>
<FieldRef Name=""Title""/>
<FieldRef Name=""Peticion""/>
<FieldRef Name=""Realizado_x0020_por""/>
<FieldRef Name=""Fecha""/>
<FieldRef Name=""Importe""/>
";
            var web = SPContext.Current.Web;
            var list = web.Lists["PresupuestoMaterial"];
            //var items = list.GetItems(query);
            //var dt = items.GetDataTable();
            //lstExpenses.DataSource = dt;
            //lstExpenses.DataBind();

            ContentIterator iterator = new ContentIterator();
            iterator.ProcessListItems(list, query, ProcessItem, ProcessError);
            lstExpenses.DataSource = dtable;
            lstExpenses.DataBind();
        }

        private bool ProcessError(SPListItem item, Exception e)
        {
            throw new NotImplementedException();
        }

        private static bool IsChecked(ListViewDataItem item)
        {
            var checkBox = item.FindControl("chkUpdate") as CheckBox;

            return checkBox.Checked;
        }

        private void UpdateItems(bool isApproved)
        {
            string status = isApproved ? "Aprobado" : "Rechazado";

            // Retrieve the selected items from the lstExpenses control.

            var selectedItems =
                from item in lstExpenses.Items
                where IsChecked(item)
                select item;

            // TODO: Ex 2 Task 1 Update the status of the list items

            var web = SPContext.Current.Web;
            var list = web.Lists["PresupuestoMaterial"];

            foreach (var selectedItem in selectedItems)
            {
                // Get the unique identifier for each list item.

                var hiddenField = selectedItem.FindControl("hdCodigo") as HiddenField;

                int itemID;

                if (int.TryParse(hiddenField.Value, out itemID))
                {

                    SPListItem item = list.GetItemById(itemID);

                    item["Estado"] = status;

                    item.Update();
                }
            }
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            UpdateItems(true);
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            UpdateItems(false);
        }
    }
}
