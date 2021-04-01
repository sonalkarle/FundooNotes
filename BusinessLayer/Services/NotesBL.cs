using BusinessLayer.Interface;
using CommanLayer.ResponseModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class NotesBL : INotesBL
    {
        

        INotesRL notesRL;

        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }



        public ResponseNoteModel AddUserNote(ResponseNoteModel note)
        {
            try
            {
               
                return notesRL.AddUserNote(note);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ICollection<ResponseNoteModel> GetActiveNotes(long UserID)
        {
            try
            {
                ICollection<ResponseNoteModel> result = notesRL.GetNotes(UserID, false, false);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteNote(long UserID, long noteID)
        {
            try
            {
                bool result = notesRL.DeleteNote(UserID, noteID);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

       

        public bool Image(int id, ImageModel image)
        {
            try
            {

                return this.notesRL.Image(id, image);                 //throw exceptions
            }

            catch (Exception e)
            {
                throw e;
            }
        }

       
        
       


        public bool Archive(long noteID, long userID)
        {
            try
            {
                return notesRL.isArchive(noteID, userID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Pin(long noteID, long userID)
        {
            try
            {
                return notesRL.isPin(noteID, userID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool SetNoteReminder(NoteReminder reminder)
        {
            try
            {
                if (reminder.ReminderOn < DateTime.Now)
                {
                    throw new Exception("Time is passed");
                }
                if (reminder.NoteID == default)
                {
                    throw new Exception("NoteID missing");
                }
                return notesRL.SetNoteReminder(reminder);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ChangeColor(long noteID, long userID, string colorCode)
        {
            try
            {
                return notesRL.ChangeColor(noteID, userID, colorCode);
            }
            catch (Exception)
            {
                throw;
            }
        }




    }

}
