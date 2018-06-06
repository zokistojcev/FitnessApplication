using FitnessApp.Models;
using FitnessApp.Repository;
using FitnessApp.ViewModels;
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
        private FitnessDb _context;

        public TrainersController()
        {
            _context = new FitnessDb();

        }


        // GET: Trainers
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ViewResult Index()
        {
            List<Trainer> trainers = _context.Trainers.ToList();
            List<TrainerViewModel> ltvm = new List<TrainerViewModel>();
            foreach (var item in trainers)
            {
                TrainerViewModel tvm = new TrainerViewModel();

                tvm.Biography = item.Biography;
                tvm.Email = item.Email;
                tvm.FullName = item.FullName;
                tvm.Id = item.Id;
                tvm.Phone = item.Phone;
                tvm.Photo = item.Photo;

                ltvm.Add(tvm);
            }

            return View(ltvm);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Trainer trainer = _context.Trainers.Single(x => x.Id == id);
            TrainerViewModel tvm = new TrainerViewModel();
            tvm.Biography = trainer.Biography;
            tvm.Email = trainer.Email;
            tvm.FullName = trainer.FullName;
            tvm.Id = trainer.Id;
            tvm.Phone = trainer.Phone;
            tvm.Photo = trainer.Photo;


            return View(tvm);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TrainerViewModel trainer)
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
                Trainer model = new Trainer();
                model.Biography = trainer.Biography;
                model.Email = trainer.Email;
                model.FullName = trainer.FullName;
                model.Id = trainer.Id;
                model.Phone = trainer.Phone;
                model.Photo = trainer.Photo;

                _context.Trainers.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trainer);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Trainer trainer = _context.Trainers.Find(id);

            TrainerViewModel tvm = new TrainerViewModel();
            tvm.Biography = trainer.Biography;
            tvm.Email = trainer.Email;
            tvm.FullName = trainer.FullName;
            tvm.Id = trainer.Id;
            tvm.Phone = trainer.Phone;
            tvm.Photo = trainer.Photo;

            return View("Edit", tvm);
        }

        [HttpPost]
        public ActionResult Edit(TrainerViewModel trainer)
        {
            if (!ModelState.IsValid)
                return View("Edit", trainer);

            var model = _context.Trainers.Find(trainer.Id);

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
                trainer.Photo = _context.Trainers.Single(x => x.Id == trainer.Id).Photo;

            model.Email = trainer.Email;
            model.Biography = trainer.Biography;
            model.FullName = trainer.FullName;
            model.Phone = trainer.Phone;
            model.Photo = trainer.Photo;

            _context.SaveChanges();

            //_context.Trainers.RemoveAt(index);
            //_context.Trainers.Insert(index, trainer);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var trainer = _context.Trainers.Find(id);

            _context.Trainers.Remove(trainer);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult SelectFromDropDown(TrainerViewModel trainer)
        {

            return View();
        }

        public ActionResult ListAllJson()
        {
            List<Trainer> trainers = _context.Trainers.ToList();
            List<TrainerViewModel> ltvm = new List<TrainerViewModel>();
            foreach (var item in trainers)
            {
                TrainerViewModel tvm = new TrainerViewModel();

                tvm.Biography = item.Biography;
                tvm.Email = item.Email;
                tvm.FullName = item.FullName;
                tvm.Id = item.Id;
                tvm.Phone = item.Phone;
                tvm.Photo = item.Photo;

                ltvm.Add(tvm);
            }

            return Json(ltvm, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListAllDropDownList()
        {
            List<Trainer> trainers = _context.Trainers.ToList();
            List<TrainerViewModel> ltvm = new List<TrainerViewModel>();
            foreach (var item in trainers)
            {
                TrainerViewModel tvm = new TrainerViewModel();

                tvm.Biography = item.Biography;
                tvm.Email = item.Email;
                tvm.FullName = item.FullName;
                tvm.Id = item.Id;
                tvm.Phone = item.Phone;
                tvm.Photo = item.Photo;

                ltvm.Add(tvm);
            }

            return View(ltvm);
        }
    }
}