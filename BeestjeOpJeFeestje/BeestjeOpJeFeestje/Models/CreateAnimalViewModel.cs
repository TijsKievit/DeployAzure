using System;
using System.Collections.Generic;
using System.Web;

namespace BeestjeOpJeFeestje.Models
{
    public class CreateAnimalViewModel
    {
        public Animal animal;
        public List<string> imageNames;
        public CreateAnimalViewModel(Animal animal, List<string> imageNames) 
        {
            this.animal = animal;
            this.imageNames = imageNames;
        }

    }
}
