using LiquadCargoManagment.Models;
using System;
using System.Linq;
using System.Web;

namespace LiquadCargoManagment.Helpers
{
    public class SecurityTokenIdentifier
    {
        private readonly LCMEntities context;
        public SecurityTokenIdentifier(LCMEntities _context)
        {
            context = _context;
        }
        public string IdentifyToken1(string Token, string FormName)
        {
            string parameter = "";
            if (Token != "" && Token != null)
            {
                try
                {
                    string _parameter = HttpUtility.UrlDecode(ApplicationHelper.IDecrypt(Token));
                    if (_parameter!=string.Empty)
                    {
                        long ID = ApplicationHelper.SplitNumberFromString(_parameter);
                        if (ID > 0)
                        {
                            var _form = context.NavMenus.Where(x => x.FormID == ID).FirstOrDefault();
                            if (_form.Url.ToLower()
                                .Contains(FormName))
                            {
                                parameter = context.RolePermissions
                                    .Where(x=>x.FormID==_form.FormID && x.RoleID == ApplicationHelper.RoleID).FirstOrDefault().Parameter;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    parameter = string.Empty;
                }
            }
            return parameter.ToString();
        }
        public string IsUserAuthenticFor(string FormName)
        {
            string parameter = "";
            try
            {
                var _form = context.NavMenus.Where(x => x.ActionName.Equals(FormName.ToLower())).FirstOrDefault();
                if (_form != null)
                {
                    parameter = context.RolePermissions
                        .Where(x => x.FormID == _form.FormID && x.RoleID == ApplicationHelper.RoleID).FirstOrDefault().Parameter;
                }
            }
            catch (Exception)
            {
                parameter = string.Empty;
            }
            return parameter.ToString();
        }

        public string IsUserAuthenticFor(long FormId)
        {
            string parameter = "";
            try
            {
                if (FormId > 0)
                {
                    parameter = context.RolePermissions
                        .Where(x => x.FormID == FormId && x.RoleID == ApplicationHelper.RoleID).FirstOrDefault().Parameter;
                }
            }
            catch (Exception)
            {
                parameter = string.Empty;
            }
            return parameter.ToString();
        }



    }
}