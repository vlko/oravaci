using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OravaciData.Commands;
using OravaciData.Model;
using vlko.core.Base;
using vlko.core.Components;
using vlko.core.RavenDB.Repository;
using vlko.core.Repository;

namespace Oravaci.Controllers
{
    public class PersonController : BaseController
    {
        [Authorize]
        public ActionResult Index(PagedModel<Person> pageModel)
        {
            pageModel.LoadData(RepositoryFactory.Command<IPersonCommand>().GetAll().OrderByDescending(item => item.FullName));
            return View(pageModel);
        }

        /// <summary>
        /// URL: Person/Edit
        /// </summary>
        /// <param name="id">The id.</param>
        [Authorize]
        public ActionResult Edit(string id)
        {
            var model = RepositoryFactory.Command<IPersonCommand>().FindByPk("people/" + id);
            return ViewWithAjax(model);
        }

        /// <summary>
        /// URL: Person/Edit
        /// </summary>
        /// <param name="model">The model.</param>
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Person model)
        {
            if (ModelState.IsValid)
            {
                using (var tran = RepositoryFactory.StartTransaction())
                {
                    var realItem = RepositoryFactory.Command<IPersonCommand>().FindByPk("people/" + model.Id);
                    realItem.FullName = model.FullName;
                    realItem.Occupation = model.Occupation;
                    realItem.Img = model.Img;
                    RepositoryFactory.Command<IPersonCommand>().Update(realItem);
                    tran.Commit();
                    SessionFactory.WaitForStaleIndexes();
                    return RedirectToActionWithAjax("Index");
                }
            }
            return ViewWithAjax(model);
        }

        /// <summary>
        /// URL: Person/Delete
        /// </summary>
        /// <param name="id">The id.</param>
        [Authorize]
        public ActionResult Delete(string id)
        {
            var model = RepositoryFactory.Command<IPersonCommand>().FindByPk("people/" + id);
            return ViewWithAjax(model);
        }

        /// <summary>
        /// URL: Person/Delete
        /// </summary>
        /// <param name="model">The model.</param>
        [Authorize]
        [HttpPost]
        public ActionResult Delete(Person model)
        {
            using (var tran = RepositoryFactory.StartTransaction())
            {
                var itemToDelete = RepositoryFactory.Command<IPersonCommand>().FindByPk("people/" + model.Id);
                RepositoryFactory.Command<IPersonCommand>().Delete(itemToDelete);
                tran.Commit();
                SessionFactory.WaitForStaleIndexes();
                return RedirectToActionWithAjax("Index");
            }
        }


        public ActionResult All()
        {
            return Json(RepositoryFactory.Command<IPersonCommand>().Top50());
        }

        public ActionResult Search(string query)
        {
            return Json(RepositoryFactory.Command<IPersonCommand>().Search(query));
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
