using Microsoft.AspNetCore.Mvc;
using MvcTodoApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MvcTodoApp.Controllers
{
    public class HomeController : Controller
    {
       // قائمة محاكاة لقاعدة البيانات (في الذاكرة)[span_4](end_span)
        private static List<TaskItem> tasks = new List<TaskItem>
        {
     new TaskItem { Id = 1, Title = "تدرب على MVC Design Pattern", IsComplete = false },
            new TaskItem { Id = 2, Title = "تدرب على N-tier Architecture", IsComplete = false },
            new TaskItem { Id = 3, Title = "تدرب على استخدام git", IsComplete = false },
        };
        /// <summary>
        /// يعرض القائمة الرئيسية للمهام.[span_8](end_span)
        /// </summary>
       public IActionResult Index()
        {
            return View(tasks);
        }

        /// <summary>
/// إضافة مهمة جديدة.[span_11](end_span)
        /// </summary>
        [HttpPost]
       public IActionResult AddTask(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
               int newId = tasks.Max(t => t.Id) + 1;
                var newTask = new TaskItem { Id = newId, Title = title, IsComplete = false };
                 tasks.Add(newTask);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
      /// تعيين مهمة كمكتملة.[span_17](end_span)
        /// </summary>
        [HttpPost]
        public IActionResult CompleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
             if (task != null)
                task.IsComplete = true;
             return RedirectToAction("Index");
        }

        // ====== إضافة كود تعديل اسم المهمة هنا ======

        /// <summary>
        /// يعرض نموذج تعديل المهمة.
        /// </summary>
        [HttpGet] // لتلقي طلبات GET لعرض النموذج
        public IActionResult EditTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound(); // إذا لم يتم العثور على المهمة، أرجع 404
            }
            return View(task); // أرسل المهمة إلى View لتعرض بياناتها الحالية
        }

        /// <summary>
        /// يعالج إرسال نموذج تعديل المهمة.
        /// </summary>
        [HttpPost] // لتلقي طلبات POST عند إرسال النموذج
        public IActionResult EditTask(int id, string newTitle)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound(); // إذا لم يتم العثور على المهمة
            }

            if (!string.IsNullOrEmpty(newTitle))
            {
                task.Title = newTitle; // تحديث اسم المهمة
            }
            
            return RedirectToAction("Index"); // العودة إلى القائمة الرئيسية بعد التعديل
        }
    }
}