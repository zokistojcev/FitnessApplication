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
        // GET: TrainingTypes
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult ListAll()
        {
            List<TrainingType> trainingTypes = _context.TrainingTypes.ToList();
            List<TrainingTypeViewModel> lttvm = new List<TrainingTypeViewModel>();
            foreach (var item in trainingTypes)
            {
                TrainingTypeViewModel ttvm = new TrainingTypeViewModel();

                ttvm.Description = item.Description;
                ttvm.Trainer = item.Trainer;
                ttvm.Name = item.Name;
                ttvm.Id = item.Id;
                ttvm.Photo = item.Photo;

                lttvm.Add(ttvm);
            }

            return View(lttvm);
            //return View(trainingTypes);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            TrainingType training = _context.TrainingTypes.Find(id);
            return View(training);
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
        public ActionResult Create(TrainingTypeViewModel viewModel)
        {
            List<Trainer> trainers = _context.Trainers.ToList();
            var ViewModel = new TrainingTypeTrainerViewModel()
            {
                TrainingType = viewModel,
                Trainers = trainers
            };
            if (ModelState.IsValid)
            {
                if (viewModel.PhotoUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(viewModel.PhotoUpload.FileName);
                    string extension = Path.GetExtension(viewModel.PhotoUpload.FileName);
                    fileName = viewModel.Name + "_" + DateTime.Now.ToString("dd-MM-yy hh-mm-ss") + extension;
                    viewModel.Photo = "~/Images/TrainingTypes/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/TrainingTypes/"), fileName);
                    viewModel.PhotoUpload.SaveAs(fileName);
                }

                TrainingType tt = new TrainingType();
                tt.Name = viewModel.Name;
                tt.Id = viewModel.Id;
                tt.Photo = viewModel.Photo;
                tt.Description = viewModel.Description;
                tt.Trainer = viewModel.Trainer;




                _context.TrainingTypes.Add(tt);
                return RedirectToAction("ListAll");
            }
            return View(viewModel);
        }
    }
}