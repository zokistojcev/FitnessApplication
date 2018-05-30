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
        // GET: TrainingTypes
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult ListAll()
        {
            List<TrainingType> trainingTypes = TrainerRepo.TrainingTypes;
            return View(trainingTypes);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            TrainingType training = TrainerRepo.TrainingTypes.Single(x => x.Id == id);
            return View(training);
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<Trainer> trainers = TrainerRepo.Trainers;
            var ViewModel = new TrainingTypeTrainerViewModel()
            {
                Trainers = trainers
            };
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult Create(TrainingTypeTrainerViewModel viewModel)
        {
            //List<Trainer> trainers = TrainerRepo.Trainers;
            var ViewModel = new TrainingTypeTrainerViewModel()
            {
                TrainingType = viewModel.TrainingType,
            };
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
                TrainerRepo.TrainingTypes.Add(viewModel.TrainingType);
                return RedirectToAction("ListAll");
            }
            return View(viewModel.TrainingType);
        }
    }
}