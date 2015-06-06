using JustBlog.Core.Objects;
using JustBlog.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustBlog.Models
{
    public class WidgetViewModel
    {
        public WidgetViewModel(IBlogRepository repository)
        {
            Categories = repository.Categories();
        }

        public IList<Category> Categories { get; private set; }
    }
}