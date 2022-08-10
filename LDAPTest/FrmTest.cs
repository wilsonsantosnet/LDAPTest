using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Windows.Forms;

namespace LDAPTest
{
    /// <summary>
    /// This Sample Code is provided for the purpose of illustration only and is not intended to be used in a production environment. THIS SAMPLE CODE AND ANY RELATED INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. We grant You a nonexclusive, royalty-free right to use and modify the Sample Code and to reproduce and distribute the object code form of the Sample Code, provided that You agree: (i)to not use Our name, logo, or trademarks to market Your software product in which the Sample Code is embedded; (ii)to include a valid copyright notice on Your software product in which the Sample Code is embedded; and(iii) to indemnify, hold harmless, and defend Us and Our suppliers from and against any claims or lawsuits, including attorneys’ fees, that arise or result from the use or distribution of the Sample Code.
    /// Please note: None of the conditions outlined in the disclaimer above will supersede the terms and conditions contained within the Premier Customer Services Description.
    /// </summary>
    public partial class FrmTest : Form
    {
        private Action<string> _atualizarTela;
        private Action _trabalhoAtual;

        public FrmTest()
        {
            InitializeComponent();
            this._atualizarTela = AtualizarTela;
   
        }
    
        private void ConectarUsuarioESenha()
        {

            string domainController = txtdomainController.Text;
            string container = txtcontainer.Text;
            string adminUser = txtadminUser.Text;
            string adminPassword = txtadminPassword.Text;
            string userName = txtuserName.Text;

            try
            {

                txtInfo.ForeColor = System.Drawing.Color.Green;
                _atualizarTela.BeginInvoke($"[{DateTime.Now}] - [info] - Inicio ConectarUsuarioESenha", null, null);

                var pc = new PrincipalContext(ContextType.Domain, domainController, container, adminUser, adminPassword);
                var user = UserPrincipal.FindByIdentity(pc, userName);
                if (user == null)
                    _atualizarTela.BeginInvoke($"[{DateTime.Now}] - [info] - Usuário {userName} não encontado", null, null);
                else
                    _atualizarTela.BeginInvoke($"[{DateTime.Now}] - [info] - Usuário {userName} encontado", null, null);

            }
            catch (Exception ex)
            {
                txtInfo.ForeColor = System.Drawing.Color.Red;
                _atualizarTela.BeginInvoke($"[{DateTime.Now}] - [error] - Ocorreu um erro em ConectarUsuarioESenha {ex.Message}", null, null);
                LogHelper.Write($"[{DateTime.Now}] - [error] - {ex.Message}");
                LogHelper.Write($"[{DateTime.Now}] - [error] - {ex.StackTrace}");

            }
        }

        private void VerificarUsuarioNoGrupos()
        {

            string domainController = txtdomainController.Text;
            string container = txtcontainer.Text;
            string adminUser = txtadminUser.Text;
            string adminPassword = txtadminPassword.Text;
            string userName = txtuserName.Text;
            string group = txtgroup.Text;

            try
            {

                txtInfo.ForeColor = System.Drawing.Color.Green;
                _atualizarTela.BeginInvoke($"[{DateTime.Now}] - [info] - Inicio VerificarUsuarioNoGrupos", null, null);

                var pc = new PrincipalContext(ContextType.Domain, domainController);
                using (var up = UserPrincipal.FindByIdentity(pc, userName))
                {
                    if (up == null)
                    {
                        _atualizarTela.BeginInvoke($"[{DateTime.Now}] - [info] - usuário {userName} não encontrado", null, null);
                        return;
                    }
                        

                    using (PrincipalSearchResult<Principal> result = up.GetGroups())
                    {
                        using (GroupPrincipal groupPrincipal = (GroupPrincipal)result.Where(g => g.SamAccountName.ToLower() == group.ToLower()).FirstOrDefault())
                        {
                            if (groupPrincipal == null)
                                _atualizarTela.BeginInvoke($"[{DateTime.Now}] - [info] - usuário {userName} não esta no grupo {group}", null, null);
                            else
                                _atualizarTela.BeginInvoke($"[{DateTime.Now}] - [info] - usuário {userName} esta no grupo {group}", null, null);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                txtInfo.ForeColor = System.Drawing.Color.Red;
                _atualizarTela.BeginInvoke($"[{DateTime.Now}] - [error] - Ocorreu um erro em VerificarUsuarioNoGrupos {ex.Message}", null, null);
                LogHelper.Write($"[{DateTime.Now}] - [error] - {ex.Message}");
                LogHelper.Write($"[{DateTime.Now}] - [error] - {ex.StackTrace}");

            }
        }


        private void VerificarUsuarioNoGruposV2()
        {

            string domainController = txtdomainController.Text;
            string container = txtcontainer.Text;
            string adminUser = txtadminUser.Text;
            string adminPassword = txtadminPassword.Text;
            string userName = txtuserName.Text;
            string group = txtgroup.Text;

            try
            {

                txtInfo.ForeColor = System.Drawing.Color.Green;
                _atualizarTela.BeginInvoke($"[{DateTime.Now}] - [info] - Inicio VerificarUsuarioNoGruposV2", null, null);


                var pc = new PrincipalContext(ContextType.Domain, domainController);
                using (var up = UserPrincipal.FindByIdentity(pc, userName))
                {
                    if (up == null)
                    {
                        _atualizarTela.BeginInvoke($"[{DateTime.Now}] - [info] - usuário {userName} não encontrado", null, null);
                        return;
                    }

                    using (PrincipalSearchResult<Principal> result = up.GetGroups(pc))
                    {
                        using (GroupPrincipal groupPrincipal = (GroupPrincipal)result.Where(g => g.SamAccountName.ToLower() == group.ToLower()).FirstOrDefault())
                        {
                            if (groupPrincipal == null)
                                _atualizarTela.BeginInvoke($"[{DateTime.Now}] - [info] - usuário {userName} não esta no grupo {group}", null, null);
                            else
                                _atualizarTela.BeginInvoke($"[{DateTime.Now}] - [info] - usuário {userName} esta no grupo {group}", null, null);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                txtInfo.ForeColor = System.Drawing.Color.Red;
                _atualizarTela.BeginInvoke($"[{DateTime.Now}] - [error] - Ocorreu um erro em VerificarUsuarioNoGruposV2 {ex.Message}", null, null);
                LogHelper.Write($"[{DateTime.Now}] - [error] - {ex.Message}");
                LogHelper.Write($"[{DateTime.Now}] - [error] - {ex.StackTrace}");

            }
        }
        private  void ConectarUsuarioESenhaDI()
        {
            string ldapString = $"LDAP://{txtdomainController.Text}/{txtcontainer.Text}";
            string adminUser = txtadminUser.Text;
            string adminPass = txtadminPassword.Text;
            string username = txtuserName.Text;

            try
            {
                txtInfo.ForeColor = System.Drawing.Color.Green;
                _atualizarTela.BeginInvoke($"[{DateTime.Now}] - [info] - Inicio ConectarUsuarioESenhaDI", null, null);

                DirectoryEntry de = new DirectoryEntry(ldapString, adminUser, adminPass, AuthenticationTypes.Secure);
                DirectorySearcher deSearch = new DirectorySearcher(de) { SearchRoot = de, Filter = txtFilter.Text };

                SearchResult searchResult = deSearch.FindOne();
                if (searchResult != null)
                    _atualizarTela.BeginInvoke($"[{DateTime.Now}] - [info] - Usuário {username}  encontrado", null, null);
                else
                    _atualizarTela.BeginInvoke($"[{DateTime.Now}] - [info] - Usuário {username} não encontrado", null, null);
            }
            catch (Exception ex)
            {
                txtInfo.ForeColor = System.Drawing.Color.Red;
                _atualizarTela.BeginInvoke($"[{DateTime.Now}] - [error] - Ocorreu um erro em ConectarUsuarioESenhaDI {ex.Message}", null, null);
                LogHelper.Write($"[{DateTime.Now}] - [error] - {ex.Message}");
                LogHelper.Write($"[{DateTime.Now}] - [error] - {ex.StackTrace}");
            }
        }

        private void AtualizarTela(string msg)
        {

            if (txtInfo.InvokeRequired)
            {
                Action safeWrite = delegate { AtualizarTela(msg); };
                txtInfo.BeginInvoke(safeWrite);
            }
            else
            {
                txtInfo.Text = txtInfo.Text + msg + System.Environment.NewLine;
                LogHelper.Write(msg);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this._trabalhoAtual = ConectarUsuarioESenha;
            this._trabalhoAtual.BeginInvoke(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this._trabalhoAtual = VerificarUsuarioNoGrupos;
            this._trabalhoAtual.BeginInvoke(null, null);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this._trabalhoAtual = VerificarUsuarioNoGruposV2;
            this._trabalhoAtual.BeginInvoke(null, null);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this._trabalhoAtual = ConectarUsuarioESenhaDI;
            this._trabalhoAtual.BeginInvoke(null, null);
        }

       
    }
}
