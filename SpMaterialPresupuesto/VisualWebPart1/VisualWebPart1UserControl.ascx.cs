using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

namespace SpMaterialPresupuesto.VisualWebPart1
{
    public partial class VisualWebPart1UserControl : UserControl
    {
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
            var items = list.GetItems(query);
            var dt = items.GetDataTable();
            lstExpenses.DataSource = dt;
            lstExpenses.DataBind();
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
