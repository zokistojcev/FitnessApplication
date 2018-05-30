using FitnessApp.Models;
using FitnessApp.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FitnessApp.Controllers
{
    public class TrainersController : Controller
    {
        // GET: Trainers
        public ActionResult Index()
        {
            return View();
        }
        public ViewResult ListAll()
        {
            List<Trainer> trainers = TrainerRepo.Trainers;
            return View(trainers);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Trainer trainer = TrainerRepo.Trainers.Single(x => x.Id == id);
            return View(trainer);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Trainer trainer)
        {

            if (ModelState.IsValid)
            {
                if (trainer.PhotoUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(trainer.PhotoUpload.FileName);
                    string extension = Path.GetExtension(trainer.PhotoUpload.FileName);
                    fileName = trainer.FullName + "_" + DateTime.Now.ToString("dd-MM-yy hh-mm-ss") + extension;
                    trainer.Photo = "~/Images/Trainers/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/Trainers/"), fileName);
                    trainer.PhotoUpload.SaveAs(fileName);
                }

                TrainerRepo.Trainers.Add(trainer);
                return RedirectToAction("ListAll");
            }
            return View(trainer);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Trainer trainer = TrainerRepo.Trainers.Single(x => x.Id == id);
            return View("Edit", trainer);
        }

        [HttpPost]
        public ActionResult Edit(Trainer trainer)
        {
            if (!ModelState.IsValid)
                return View("Edit", trainer);

            int index = TrainerRepo.Trainers.FindIndex(t => t.Id == trainer.Id);

            if (trainer.PhotoUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(trainer.PhotoUpload.FileName);
                string extension = Path.GetExtension(trainer.PhotoUpload.FileName);
                fileName = trainer.FullName + "_" + DateTime.Now.ToString("dd-MM-yy hh-mm-ss") + extension;
                trainer.Photo = "~/Images/Trainers/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/Trainers/"), fileName);
                trainer.PhotoUpload.SaveAs(fileName);
            }
            else
                trainer.Photo = TrainerRepo.Trainers.Single(x => x.Id == trainer.Id).Photo;

            TrainerRepo.Trainers.RemoveAt(index);
            TrainerRepo.Trainers.Insert(index, trainer);

            return RedirectToAction("ListAll");
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            int index = TrainerRepo.Trainers.FindIndex(t => t.Id == id);
            TrainerRepo.Trainers.RemoveAt(index);

            return RedirectToAction("ListAll");
        }
    }
}