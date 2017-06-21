using System.Windows.Forms;
using Microsoft.Deployment.WindowsInstaller;

namespace MyCustomAction
{
    public class CustomActions
    {
        [CustomAction]
        public static ActionResult CreateDatabase(Session session)
        {
            CreateDatabase createDbForm = new CreateDatabase();
            if (createDbForm.ShowDialog() == DialogResult.Cancel)
                return ActionResult.UserExit;

            return ActionResult.Success;
        }
    }
}
