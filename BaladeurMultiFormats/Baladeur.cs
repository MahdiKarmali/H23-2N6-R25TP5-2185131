using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaladeurMultiFormats
{
    public class Baladeur : IBaladeur
    {

        #region Champs

        private const string NOM_RÉPERTOIRE = "Chansons";
        private List<Chanson> m_colChansons;


        #endregion

        #region Propriété

        public int NbChansons { get; }

        #endregion

        #region Constructeurs

        /// <summary>
        /// Initialise une instance de la classe Baladeur. Elle instancie la collection des chansons.
        /// </summary>
        Baladeur()
        {
            m_colChansons = new List<Chanson>();
        }

        #endregion


        #region Méthodes

        /// <summary>
        /// Affiche la liste des chansons dans la pListView passée en paramètre.
        /// </summary>
        /// <param name="pListView"></param>
        public void AfficherLesChansons(ListView pListView)
        {
            foreach (Chanson chanson in m_colChansons)
            {
                ListViewItem objListView = new ListViewItem(chanson.Artiste);
                objListView.SubItems.Add(chanson.Titre);
                objListView.SubItems.Add(chanson.Annee.ToString());
                objListView.SubItems.Add(chanson.Format);

                pListView.Items.Add(objListView);
            }
        }

        public Chanson ChansonAt(int pIndex)
        {
            return m_colChansons[pIndex];
        }

        /// <summary>
        /// Charge la liste des chansons dans m_colChansons. Elle doit vérifier l’existence du répertoire des chanson
        /// Ensuite lit chaque fichier et instancie une classe de chanson selon le format et l’ajoute dans la collection des chansons m_colChansons 
        /// </summary>
        public void ConstruireLaListeDesChansons()
        {

            if (Directory.Exists(NOM_RÉPERTOIRE))
            {

                foreach (string fichier in Directory.GetFiles(NOM_RÉPERTOIRE))
                {

                    Chanson chanson = null;

                    try
                    {
                        switch (fichier.Split('.')[1].ToUpper())
                        {
                            case "AAC":
                                chanson = new ChansonAAC(fichier);
                                break;
                            case "MP3":
                                chanson = new ChansonMP3(fichier);
                                break;
                            case "WMA":
                                chanson = new ChansonWMA(fichier);
                                break;
                        }
                    }

                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }


                    if (chanson != null)
                    {
                        m_colChansons.Add(chanson);
                    }
                }

            }


        }

        /// <summary>
        /// Instancie une ChansonAAC à partir de la chanson à l’index pIndex, enregistre les paroles et supprime le fichier du format précédent.
        /// Elle utilise la méthode Ecrire pour enregistrer les paroles et la méthode File.Delete pour supprimer un fichier.
        /// </summary>
        /// <param name="pIndex"></param>
        public void ConvertirVersAAC(int pIndex)
        {
            Chanson objChanson = ChansonAt(pIndex);

            if (objChanson.Format == "AAC")
            {
                return;
            }

            ChansonAAC objChansonAAC = new ChansonAAC(NOM_RÉPERTOIRE, objChanson.Artiste, objChanson.Titre, objChanson.Annee);
            objChansonAAC.Ecrire(objChanson.Paroles);

            File.Delete(objChanson.NomFichier);
            m_colChansons.RemoveAt(pIndex);
        }

        /// <summary>
        /// Instancie une ChansonMP3 à partir de la chanson à l’index pIndex, enregistre les paroles et supprime le fichier du format précédent.
        /// Elle utilise la méthode Ecrire pour enregistrer les paroles et la méthode File.Delete pour supprimer un fichier.
        /// </summary>
        /// <param name="pIndex"></param>
        public void ConvertirVersMP3(int pIndex)
        {
            Chanson objChanson = ChansonAt(pIndex);

            if (objChanson.Format == "MP3")
            {
                return;
            }

            ChansonMP3 chansonMP3 = new ChansonMP3(NOM_RÉPERTOIRE, objChanson.Artiste, objChanson.Titre, objChanson.Annee);
            chansonMP3.Ecrire(objChanson.Paroles);

            File.Delete(objChanson.NomFichier);
            m_colChansons.RemoveAt(pIndex);
        }

        /// <summary>
        /// Instancie une ChansonWMA à partir de la chanson à l’index pIndex, enregistre les paroles et supprime le fichier du format précédent.
        /// Elle utilise la méthode Ecrire pour enregistrer les paroles et la méthode File.Delete pour supprimer un fichier.
        /// </summary>
        /// <param name="pIndex"></param>
        public void ConvertirVersWMA(int pIndex)
        {
            Chanson objChanson = ChansonAt(pIndex);

            if (objChanson.Format == "WMA")
            {
                return;
            }

            ChansonWMA chansonWMA = new ChansonWMA(NOM_RÉPERTOIRE, objChanson.Artiste, objChanson.Titre, objChanson.Annee);
            chansonWMA.Ecrire(objChanson.Paroles);

            File.Delete(objChanson.NomFichier);
            m_colChansons.RemoveAt(pIndex);
        }

        #endregion


    }
}
