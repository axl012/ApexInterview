using Entrevista.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entrevista.PageObjects
{
    class Elements : BasePage
    {
        public Elements(BasePage page) : base("\\PageObjects\\Elements.json", page)
        {
        }
    }
}
