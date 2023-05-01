using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaladeurMultiFormats
{
    public abstract class Chanson : IChanson
    {

        #region Champs

        protected int m_annee;
        protected string m_artiste;
        protected string m_nomFichier;
        protected string m_titre;

        #endregion



        #region Propriétés


        public int Annee { get; }

        public string Artiste { get; }

        public string Format { get; }

        public string NomFichier { get; }

        public string Paroles { get; }

        public string Titre { get; }

        #endregion


        #region Constructeurs

        Chanson(string pNomFichier)
        {

        }
        
        Chanson(string pRepertoire, string pArtiste, string pTitre, int pAnnée)
        {

        }

        #endregion



        #region Méthodes


        public void Ecrire(string pParoles)
        {
            throw new NotImplementedException();
        }

        public void EcrireEntete(StreamWriter pobjFichier)
        {
            throw new NotImplementedException();
        }

        public void EcrireParoles(StreamWriter pobjFichier, string pParoles)
        {
            throw new NotImplementedException();
        }

        public void LireEntete()
        {
            throw new NotImplementedException();
        }

        public string LireParoles(StreamReader pobjFichier)
        {
            throw new NotImplementedException();
        }

        public void SauterEntete(StreamReader pobjFichier)
        {
            throw new NotImplementedException();
        }

        #endregion



    }
}
