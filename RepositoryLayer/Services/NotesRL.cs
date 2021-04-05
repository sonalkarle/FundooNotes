using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommanLayer.ResponseModel;
using CommonLayer.NoteException;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class NotesRL : INotesRL
    {
        readonly FundooApiContext notesContext;
        ICollection<ResponseNoteModel> responseNoteModels;
        public NotesRL(FundooApiContext context)
        {
            notesContext = context;
        }
        public ICollection<ResponseNoteModel> GetNotes(long UserID)
        {
            try
            { 
               responseNoteModels = notesContext.Notes.Where(N => N.UserId.Equals(UserID)).OrderBy(N => N.CreatedOn).Select(N =>
                 new ResponseNoteModel
                 {
                    UserID = (long)N.UserId,
                    NoteId = N.NoteId,
                    Title = N.Title,
                    Body = N.Body,
                    Color = N.Color,
                    Image = N.Image,
                    Reminder =(DateTime) N.Reminder,
                    isPin = (bool) N.IsPin,
                    isArchieve = (bool)N.IsArchieve,
                    isTrash =(bool) N.IsTrash,
                   
                 }
                 ).ToList();
                 return responseNoteModels; 
            }
            catch (Exception ex)
            {
                throw ex;
                    
            }
        }

        public ResponseNoteModel DeleteNote(long UserID)
        {
            try
            {
                var note1 = notesContext.Notes.Where(N => N.UserId.Equals(UserID)).OrderBy(N => N.CreatedOn).LastOrDefault();
                var noteID = note1.NoteId;
                var note = notesContext.Notes.Find(noteID);


                if (notesContext.Notes.Any(n => n.NoteId == noteID && n.UserId == UserID ))
                {
                    if (note.IsTrash == true)
                    {
                        notesContext.Entry(note).State = EntityState.Deleted;
                        return null;
                    }
                    else
                    {
                        note.IsTrash = true;
                        ResponseNoteModel NewNote = new ResponseNoteModel
                        {
                            UserID = (long)note.UserId,
                            NoteId = note.NoteId,
                            Title = note.Title,
                            Body = note.Body,
                            Color = note.Color,
                            Image = note.Image,
                            Reminder = (DateTime)note.Reminder,
                            isPin = (bool)note.IsPin,
                            isArchieve = (bool)note.IsArchieve,
                            isTrash = (bool)note.IsTrash,
                           
                        };
                        notesContext.SaveChanges();
                        return NewNote;
                    }
                }
                else
                {
                    throw new NoteException(NoteException.ExceptionType.WRONG_NOTEID, "Note Id does not exist");
                }
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseNoteModel> Archive(long UserID)
        {
            var note1 = notesContext.Notes.Where(N => N.UserId.Equals(UserID)).OrderBy(N => N.CreatedOn).LastOrDefault();
            var noteID = note1.NoteId;

            var note = this.notesContext.Notes.Where(n => n.NoteId == noteID && n.IsTrash == false).SingleOrDefault();
            if (note != null)
            {
                if (note.IsArchieve == true)
                {
                    note.IsArchieve = false;


                    ResponseNoteModel NewNote = new ResponseNoteModel
                    {
                        UserID = (long)note.UserId,
                        NoteId = note.NoteId,
                        Title = note.Title,
                        Body = note.Body,
                        Color = note.Color,
                        Image = note.Image,
                        Reminder = (DateTime)note.Reminder,
                        isPin = (bool)note.IsPin,
                        isArchieve = (bool)note.IsArchieve,
                        isTrash = (bool)note.IsTrash,

                    };
                    await this.notesContext.SaveChangesAsync();
                    return NewNote;
                }
                else
                {
                    note.IsArchieve = true;
                    ResponseNoteModel NewNote = new ResponseNoteModel
                    {
                        UserID = (long)note.UserId,
                        NoteId = note.NoteId,
                        Title = note.Title,
                        Body = note.Body,
                        Color = note.Color,
                        Image = note.Image,
                        Reminder = (DateTime)note.Reminder,
                        isPin = (bool)note.IsPin,
                        isArchieve = (bool)note.IsArchieve,
                        isTrash = (bool)note.IsTrash,

                    };
                    await this.notesContext.SaveChangesAsync();
                    return NewNote;

                }
            }
            else
            {
                throw new NoteException(NoteException.ExceptionType.NOTES_NOT_EXIST, "Note does not exist");
            }
          
        }

       

        public List<Note> ArchiveList()
        {
            var result = from notes in notesContext.Notes  where notes.IsArchieve == true select notes;
            if (result != null)
            {
                return notesContext.Notes.Where(list => list.IsArchieve == true).ToList();
            }
            return null;
        }


        public async Task<ResponseNoteModel> pin(long UserID)
        {
            var note1 = notesContext.Notes.Where(N => N.UserId.Equals(UserID)).OrderBy(N => N.CreatedOn).LastOrDefault();
            var noteID = note1.NoteId;

            var note = this.notesContext.Notes.Where(n => n.NoteId == noteID && n.IsArchieve == false && n.IsTrash == false).SingleOrDefault();
            if (note != null)
            {
                if (note.IsPin == true)
                {
                    note.IsPin = false;
                    ResponseNoteModel NewNote = new ResponseNoteModel
                    {
                        UserID = (long)note.UserId,
                        NoteId = note.NoteId,
                        Title = note.Title,
                        Body = note.Body,
                        Color = note.Color,
                        Image = note.Image,
                        Reminder = (DateTime)note.Reminder,
                        isPin = (bool)note.IsPin,
                        isArchieve = (bool)note.IsArchieve,
                        isTrash = (bool)note.IsTrash,
                        
                    };

                    await this.notesContext.SaveChangesAsync();
                    return NewNote;
                }
                else
                {
                    note.IsPin = true;
                    ResponseNoteModel NewNote = new ResponseNoteModel
                    {
                        UserID = (long)note.UserId,
                        NoteId = note.NoteId,
                        Title = note.Title,
                        Body = note.Body,
                        Color = note.Color,
                        Image = note.Image,
                        Reminder = (DateTime)note.Reminder,
                        isPin = (bool)note.IsPin,
                        isArchieve = (bool)note.IsArchieve,
                        isTrash = (bool)note.IsTrash,
                       
                    };

                    await this.notesContext.SaveChangesAsync();
                    return NewNote;
                }
            }
            else
            {
                throw new NoteException(NoteException.ExceptionType.NOTES_NOT_EXIST, "Note does not exist");
            }
          
        }

       

       
       
        public ResponseNoteModel SetNoteReminder(long UserID, DateTime reminder)
        {
            try
            {
                var note1 = notesContext.Notes.Where(N => N.UserId.Equals(UserID)).OrderBy(N => N.CreatedOn).LastOrDefault();
                var noteID = note1.NoteId;

                var note = notesContext.Notes.FirstOrDefault(N => N.NoteId == noteID && N.IsTrash==false);
                if (note != null)
                {
                    note.Reminder = reminder;
                    ResponseNoteModel NewNote = new ResponseNoteModel
                    {
                        UserID =(long) note.UserId,
                        NoteId = note.NoteId,
                        Title = note.Title,
                        Body = note.Body,
                        Color = note.Color,
                        Image = note.Image,
                        Reminder = (DateTime)note.Reminder,
                        isPin = (bool)note.IsPin,
                        isArchieve = (bool)note.IsArchieve,
                        isTrash = (bool)note.IsTrash,
                        
                    };
                    notesContext.SaveChanges();
                    return NewNote;
                }
                else
                {
                    throw new NoteException(NoteException.ExceptionType.NOTES_NOT_EXIST, "Note Id does not exist");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Note AddUserNote(ResponseNoteModel note)
        {
            try
            {
                var NewNote = new Note
                {

                    UserId = note.UserID,
                    Title = note.Title,
                    Body = note.Body,
                    Color = note.Color,
                    Image = note.Image,
                    Reminder = note.Reminder,
                    IsPin = note.isPin,
                    IsArchieve = note.isArchieve,
                    IsTrash = note.isTrash,
                   
                };
                notesContext.Notes.Add(NewNote);
                notesContext.SaveChanges();
                return NewNote;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
       

        public async Task<ResponseNoteModel> ChangeColor(long UserID, string color)
        {
            var note1 = notesContext.Notes.Where(N => N.UserId.Equals(UserID)).OrderBy(N => N.CreatedOn).LastOrDefault();
            var noteID = note1.NoteId;
            var note = this.notesContext.Notes.Where(n => n.NoteId == noteID && n.IsTrash == false).SingleOrDefault();
            if (note != null)
            {
                if (color != null)
                {
                    note.Color = color;
                    this.notesContext.Notes.Update(note);
                     
                        ResponseNoteModel NewNote = new ResponseNoteModel
                        {
                            UserID = (long)note.UserId,
                            NoteId = note.NoteId,
                            Title = note.Title,
                            Body = note.Body,
                            Color = note.Color,
                            Image = note.Image,
                            Reminder = (DateTime)note.Reminder,
                            isPin = (bool)note.IsPin,
                            isArchieve = (bool)note.IsArchieve,
                            isTrash = (bool)note.IsTrash,
                           
                        };
                    await notesContext.SaveChangesAsync();
                    return NewNote;
                   
                }
                else
                {
                    throw new NoteException(NoteException.ExceptionType.NOTES_NOT_EXIST, "Note does not exist");
                }
                
            }
            throw new Exception();
        }

        public async Task<ResponseNoteModel> UploadImage(IFormFile file, long UserID)
        {
            var note1 = notesContext.Notes.Where(N => N.UserId.Equals(UserID)).OrderBy(N => N.CreatedOn).LastOrDefault();
            var noteID = note1.NoteId;
            var note = this.notesContext.Notes.Where(t => t.NoteId == noteID).SingleOrDefault();
            var stream = file.OpenReadStream();
            var name = file.FileName;
            Account account = new Account("devjs8e7v", "217619793785761", "t8nmfVwKgMJciXM8dP_B2C5UK90");
            Cloudinary cloudinary = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(name, stream)
            };
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            note.Image = uploadResult.Url.ToString();
            try
            {
                ResponseNoteModel NewNote = new ResponseNoteModel
                {
                    UserID = (long)note.UserId,
                    NoteId = note.NoteId,
                    Title = note.Title,
                    Body = note.Body,
                    Color = note.Color,
                    Image = note.Image,
                    Reminder = (DateTime)note.Reminder,
                    isPin = (bool)note.IsPin,
                    isArchieve = (bool)note.IsArchieve,
                    isTrash = (bool)note.IsTrash,

                };
                await notesContext.SaveChangesAsync();
                return NewNote;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


       

      
       
       

       

        public async Task<string> EmptyTrash()
        {
            var result = from notes in this.notesContext.Notes where notes.IsTrash == true select notes;
            if (result != null)
            {
                foreach (var note in result)
                {
                    this.notesContext.Notes.Remove(note);
                }
                var res = await this.notesContext.SaveChangesAsync();
                return "Trash removed"; ;
            }
            return default;
        }

       
        public async Task<ResponseNoteModel> Restore(long UserID)
        {
            var note1 = notesContext.Notes.Where(N => N.UserId.Equals(UserID)).OrderBy(N => N.CreatedOn).LastOrDefault();
            var noteID = note1.NoteId;
            var note = this.notesContext.Notes.Where(note => note.NoteId == noteID && note.IsTrash == true).SingleOrDefault();
            if (note != null)
            {
                note.IsTrash = false;
                ResponseNoteModel NewNote = new ResponseNoteModel
                {
                    UserID = (long)note.UserId,
                    NoteId = note.NoteId,
                    Title = note.Title,
                    Body = note.Body,
                    Color = note.Color,
                    Image = note.Image,
                    Reminder = (DateTime)note.Reminder,
                    isPin = (bool)note.IsPin,
                    isArchieve = (bool)note.IsArchieve,
                    isTrash = (bool)note.IsTrash,

                };
                await notesContext.SaveChangesAsync();
                return NewNote;
            }
            else
            {
                throw new NoteException(NoteException.ExceptionType.NOTES_NOT_EXIST, "Note does not exist");
            }
            
        }

    }
}
