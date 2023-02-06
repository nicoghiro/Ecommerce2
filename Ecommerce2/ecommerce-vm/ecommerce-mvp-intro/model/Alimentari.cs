using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_oop_ecommerce_basic.model
{
    public class Alimentari : Product
    {
        DateTime creazione { set; get; }
        DateTime scadenza { set; get; }
        private int lung=0;
        private string[] ingredienti = new string[10];
       public string[] Ingredienti
        {
            get
            {
                return ingredienti;
            }
            set
            {
                for(int i = 0; i < value.Length; i++)
                {
                    if (value[i] != null)
                    {
                        ingredienti[lung]=value[i];
                        lung++;
                    }
                }
            }
        }
            
        public Alimentari(string id, string name, string prod, string descr, float price, string[] ingredienti,DateTime Scadenza) : base(id, name, prod, descr, price)
        {
            this.ingredienti = ingredienti; 
            creazione = DateTime.Now;
            scadenza = Scadenza;
        }
        public Alimentari(string id, string name, string prod, string descro, string[] ingredienti,DateTime Scadenza) : base(id, name, prod, descro)
        {
            this.ingredienti = ingredienti;
            creazione = DateTime.Now;
            scadenza = Scadenza;
        }
        public Alimentari(Alimentari prodotto) : base(prodotto)
        {
            ingredienti = null;
            creazione = DateTime.Now;
            scadenza = DateTime.Now;
            scadenza.AddDays(14);
        }
        public Alimentari(string id) : base(id)
        {
          ingredienti=null;
            creazione = DateTime.Now;
        }
        override public Product Clone()
        {
            return new Alimentari(this);
        }
       override public float getScontato()
       {
        if (DateTime.Now >scadenza.AddDays(-7) )
        {
                return base.getScontato() / 100 * 50;
        }
        return base.getScontato();
        
    }
    }
   
}
