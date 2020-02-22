using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowProcess.Models;

namespace WorkflowProcess.Repository
{
    public class PasswordConcrete : IPassword
    {
        private readonly DatabaseContext _context;
        public PasswordConcrete(DatabaseContext context)
        {
            _context = context;
        }
        public long? SavePassword(PasswordMaster passwordMaster)
        {
            try
            {
                long? result = -1;
                if (passwordMaster != null)
                {
                    _context.PasswordMaster.Add(passwordMaster);
                    _context.SaveChanges();
                    result = passwordMaster.PasswordId;
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetPasswordbyUserId(string userEmail)
        {
            try
            {
                var password = (from passwordmaster in _context.PasswordMaster
                                where passwordmaster.UserEmail == userEmail
                                select passwordmaster.Password).FirstOrDefault();

                return password;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public PasswordMaster GetCheckUserEmailExists(string userEmail)
        {
            try
            {
                var password = (from passwordmaster in _context.PasswordMaster
                                where passwordmaster.UserEmail == userEmail
                                select passwordmaster).FirstOrDefault();

                return password;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool CheckEmailExists(string customerEmail)
        {
            try
            {
                var result = (from menu in _context.PasswordMaster
                              where menu.UserEmail == customerEmail
                              select menu).Any();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
