using CommanLayer.ResponseModel;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.ContextFile;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NotesRL : INotesRL
    {
        readonly UserContext notesContext;
        ICollection<ResponseNoteModel> responseNoteModels;


        public NotesRL(UserContext context)
        {
            notesContext = context;
        }


        public ICollection<ResponseNoteModel> GetNotes(long UserID, bool IsTrash, bool IsArchieve)
        {
            try
            {
                responseNoteModels = notesContext.Note.Where(N => N.UserID.Equals(UserID)
                && N.isTrash == IsTrash && N.isArchieve == IsArchieve).OrderBy(N => N.CreatedOn).Select(N =>
                    new ResponseNoteModel
                    {
                        UserID = (long)N.UserID,
                        NoteId = N.NoteId,
                        Title = N.Title,
                        Body = N.Body,
                        Color = N.Color,
                        Image = N.Image,
                        Reminder = N.Reminder,
                        isPin = N.isPin,
                        isArchieve = N.isArchieve,
                        isTrash = N.isTrash
                    }
                    ).ToList();
                return responseNoteModels;
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
                if (notesContext.Note.Any(n => n.NoteId == noteID && n.UserID == UserID))
                {
                    var note = notesContext.Note.Find(noteID);
                    if (note.isTrash)
                    {
                        notesContext.Entry(note).State = EntityState.Deleted;
                    }
                    else
                    {
                        note.isTrash = true;
                        note.isPin = false;
                        note.isArchieve = false;
                    }
                    notesContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool isArchive(long noteID, long userID)
        {
            try
            {
                var note = notesContext.Note.FirstOrDefault(N => N.NoteId == noteID && N.UserID == userID);
                if (note.isArchieve)
                {
                    note.isArchieve = false;
                }
                else
                {
                    note.isArchieve = true;
                }
                notesContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
       
        public bool isPin(long noteID, long userID)
        {
            try
            {
                var note = notesContext.Note.FirstOrDefault(N => N.NoteId == noteID && N.UserID == userID);
                if (note.isPin)
                {
                    note.isPin = false;
                }
                else
                {
                    note.isPin = true;
                }
                notesContext.SaveChanges();
                return true;
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
                var note = notesContext.Note.FirstOrDefault(N => N.NoteId == noteID && N.UserID == userID);
                if (colorCode != null)
                {
                    note.Color = "#" + colorCode;
                }
                notesContext.SaveChanges();
                return true;
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
                notesContext.Note.FirstOrDefault(
                    N => N.NoteId == reminder.NoteID && N.UserID == reminder.UserID).ReminderOn = reminder.ReminderOn;
                notesContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ResponseNoteModel AddUserNote(ResponseNoteModel note)
        {
            try
            {
                var NewNote = new Note
                {
                    UserID = note.UserID,
                    Title = note.Title,
                    Body = note.Body,
                    Color = note.Color,
                    Image = note.Image,
                    Reminder = note.Reminder,
                    isPin = note.isPin,
                    isArchieve = note.isArchieve,
                    isTrash = note.isTrash,
                };
                notesContext.Note.Add(NewNote);
                notesContext.SaveChanges();
                return note;
            }
                      
            catch (Exception)
            {
                throw;
            }



        }



      
        public bool Image(int id, ImageModel image)
        {

            var updateImage = notesContext.Note.FirstOrDefault(e => e.NoteId == id);
            updateImage.Image = image.Image;

            notesContext.SaveChanges();

            if (notesContext.Note
                .FirstOrDefault(e => e.NoteId == updateImage.NoteId) != null)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

       
       

    }
}
