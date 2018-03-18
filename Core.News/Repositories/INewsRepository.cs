using Core.News.Web.Entities;
using System;
using System.Collections.Generic;

namespace Core.News
{
    public interface INewsRepository
    {
        Category AddUpdateCategory(Category category);
        Item AddUpdateItem(Item item);
        ItemContent AddUpdateStory(Category category, ItemContent content);
        DateTime GetLastContentDate();        
    }
}
