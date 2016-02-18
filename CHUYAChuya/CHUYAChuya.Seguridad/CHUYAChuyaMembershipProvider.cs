using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Configuration;
//using System.DirectoryServices;
using CHUYAChuya.EntidadesNegocio;
using CHUYAChuya.LogicaNegocio;


namespace CHUYAChuya.Seguridad
{
    public class CHUYAChuyaMembershipProvider : MembershipProvider
    {
        #region NotImplementedException

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

#endregion

        public override bool ValidateUser(string username, string password)
        {
            try
            {
                //DirectoryEntry objDirectoryEntry;
                string usu = "";
                string cla = "";

                usu = password.Substring(0, 4).ToUpper();
                if (usu == "AMDO")
                {
                    cla = password.Substring(4, password.Length - 4);
                }
                else
                {
                    usu = username;
                    cla = password;
                }

                //objDirectoryEntry = new DirectoryEntry(_path, usu, cla);
                //DirectorySearcher objDirectorySearcher = new DirectorySearcher(objDirectoryEntry);

                //DirectoryEntry _objUser;
                //SearchResult _objSearchResult;

                //objDirectorySearcher.Filter = "(SAMAccountName=" + username + ")";
                //_objSearchResult = objDirectorySearcher.FindOne();
                //_objUser = _objSearchResult.GetDirectoryEntry();

                SeguridadLN oSeguridadLN = new SeguridadLN();
                //ConstSistemaLN oConstSistemaLN = new ConstSistemaLN();


                Usuario oUsuarioIni = new Usuario();
                oUsuarioIni.cNomUsuario = username;

                bool validar = false;
                //if (_objUser != null)
                {
                    Usuario oUsuario = new Usuario();
                    validar = true;
                    //string fecha = oConstSistemaLN.DevolverValor(16);
                    oUsuario = oSeguridadLN.ObtenerDatosUsuario(oUsuarioIni);
                    oUsuario.NombrePC = "CMACMAYNAS";// FunGlobales.ObtenerNombrePC();
                    //oUsuario.IpPC = FunGlobales.ObtenerIpPC();

                    HttpContext.Current.Session["Datos"] = oUsuario;
                    //HttpContext.Current.Session["W1"] = FunGlobales.base64Encode(usu);
                    //HttpContext.Current.Session["W2"] = FunGlobales.base64Encode(cla);

                    //HttpContext.Current.Session["FecSis"] = new DateTime(Int32.Parse(fecha.Substring(6, 4)), Int32.Parse(fecha.Substring(3, 2)), Int32.Parse(fecha.Substring(0, 2)));
                }
                return validar;
            }
            catch
            {
                return false;
            }
        }
    }
}
