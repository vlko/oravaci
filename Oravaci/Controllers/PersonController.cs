using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OravaciData.Commands;
using OravaciData.Model;
using vlko.core.Repository;

namespace Oravaci.Controllers
{
    public class PersonController : Controller
    {
        public ActionResult All()
        {
            using (RepositoryFactory.StartUnitOfWork())
            {
                return Json(RepositoryFactory.Command<IPersonCommand>().GetAll());
            }
        }

        public ActionResult Search(string query)
        {
            using (RepositoryFactory.StartUnitOfWork())
            {
                return Json(RepositoryFactory.Command<IPersonCommand>().Search(query));
            }
        }

        public ActionResult Store(Person person)
        {
            using (var tran = RepositoryFactory.StartTransaction())
            {
                if (string.IsNullOrEmpty(person.Img))
                {
                    person.Img = "/Content/images/anonym.png";
                }

                if (string.IsNullOrEmpty(person.Id))
                {
                    RepositoryFactory.Command<IPersonCommand>().Create(person);
                }
                else
                {
                    RepositoryFactory.Command<IPersonCommand>().Update(person);
                }

                tran.Commit();
            }
            return Json(new { result = true });
        }
    }
}
