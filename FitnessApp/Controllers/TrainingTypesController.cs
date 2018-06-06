using FitnessApp.Models;
using FitnessApp.Repository;
using FitnessApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FitnessApp.Controllers
{
    public class TrainingTypesController : Controller
    {

        private FitnessDb _context;

        public TrainingTypesController()
        {
            _context = new FitnessDb();

        }

        public ViewResult Index()
        {

            List<TrainingType> trainingTypes = new List<TrainingType>();
            
            trainingTypes = _context.TrainingTypes.ToList();
            List<TrainingTypeIndexViewModel> lttvm = new List<TrainingTypeIndexViewModel>();
            foreach (var item in trainingTypes)
            {

                var tr = _context.Trainers.Find(item.TrainerId);
                TrainingTypeIndexViewModel ttvm = new TrainingTypeIndexViewModel();

                ttvm.Description = item.Description;
                var hh = new Trainer();
                ttvm.Trainer = hh;

                ttvm.Trainer.FullName = tr.FullName;
                ttvm.Trainer.Email = tr.Email;
                ttvm.Trainer.Phone = tr.Phone;
                ttvm.Name = item.Name;
                ttvm.Id = item.Id;
                ttvm.Photo = item.Photo;

                lttvm.Add(ttvm);
            }

            return View(lttvm);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            TrainingType training = new TrainingType();
            training = _context.TrainingTypes.Find(id);
            Trainer trainer = new Trainer();
            trainer = _context.Trainers.Find(training.TrainerId);
            training.Trainer = new Trainer();
            TrainingTypeDetailsViewModel tt = new TrainingTypeDetailsViewModel();
            tt.Trainer = new Trainer();
            tt.Trainer.FullName = trainer.FullName;
            tt.Name = training.Name;
            tt.Photo = training.Photo;
            tt.Id = training.Id;
            return View(tt);
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<Trainer> trainers = _context.Trainers.ToList();
            var ViewModel = new TrainingTypeTrainerViewModel()
            {
                Trainers = trainers
            };
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult Create(TrainingTypeTrainerViewModel viewModel)
        {         
            if (ModelState.IsValid)
            {
                if (viewModel.TrainingType.PhotoUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(viewModel.TrainingType.PhotoUpload.FileName);
                    string extension = Path.GetExtension(viewModel.TrainingType.PhotoUpload.FileName);
                    fileName = viewModel.TrainingType.Name + "_" + DateTime.Now.ToString("dd-MM-yy hh-mm-ss") + extension;
                    viewModel.TrainingType.Photo = "~/Images/TrainingTypes/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/TrainingTypes/"), fileName);
                    viewModel.TrainingType.PhotoUpload.SaveAs(fileName);
                }              
            }
            
            int t = viewModel.TrainingType.TrainerId;
            var yy = _context.Trainers.Find(t);

            var model = new TrainingType()
            {
                Description = viewModel.TrainingType.Description,
                Name = viewModel.TrainingType.Name,

                Photo = viewModel.TrainingType.Photo,
                Trainer = yy
            };

            _context.TrainingTypes.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
            
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var trainingTypes = _context.TrainingTypes.Find(id);

            _context.TrainingTypes.Remove(trainingTypes);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            TrainingType modelTrainingType = new TrainingType();
            modelTrainingType = _context.TrainingTypes.Find(id);
            var modelTrainer = _context.Trainers.Find(modelTrainingType.TrainerId);
            modelTrainingType.Trainer = new Trainer();
            modelTrainingType.Trainer = modelTrainer;

            return View(modelTrainingType);
        }



        [HttpPost]
        public ActionResult Edit(TrainingTypeViewModel trainingType)
        {
            if (!ModelState.IsValid)
                return View("Edit", trainingType);

            var modelTrainingType = _context.TrainingTypes.Find(trainingType.Id);
            var model2 = _context.Trainers.Find(modelTrainingType.TrainerId);
            if (trainingType.PhotoUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(trainingType.PhotoUpload.FileName);
                string extension = Path.GetExtension(trainingType.PhotoUpload.FileName);
                fileName = trainingType.Name + "_" + DateTime.Now.ToString("dd-MM-yy hh-mm-ss") + extension;
                trainingType.Photo = "~/Images/Trainers/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/Trainers/"), fileName);
                trainingType.PhotoUpload.SaveAs(fileName);
            }
            else
                trainingType.Photo = _context.Trainers.Single(x => x.Id == trainingType.Id).Photo;

            modelTrainingType.Id = trainingType.Id;
            modelTrainingType.Description = trainingType.Description;
            modelTrainingType.Name = trainingType.Name;
            modelTrainingType.Photo = trainingType.Photo;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}