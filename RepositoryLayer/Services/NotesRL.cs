using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommanLayer;
using CommanLayer.ResponseModel;
using CommonLayer.NoteException;
using CommonLayer.RequestModel;
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
                      Reminder = (DateTime)N.Reminder,
                      isPin = (bool)N.IsPin,
                      isArchieve = (bool)N.IsArchieve,
                      isTrash = (bool)N.IsTrash,

                  }
                  ).ToList();
                return responseNoteModels;
            }
            catch (Exception )
            {
                throw ;

            }
        }

        public ResponseNoteModel DeleteNote(long UserID,long noteID)
        {
            try
            {
               
                var note = notesContext.Notes.Find(noteID);


                if (notesContext.Notes.Any(n => n.NoteId == noteID && n.UserId == UserID))
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
            catch (Exception )
            {
                throw;
            }
        }

        public async Task<ResponseNoteModel> Archive(long UserID,long noteID)
        {
            try
            {

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
            catch (Exception)
            {
                throw;
            }

        }



        public List<Note> ArchiveList()
        {
            var result = from notes in notesContext.Notes where notes.IsArchieve == true select notes;
            if (result != null)
            {
                return notesContext.Notes.Where(list => list.IsArchieve == true).ToList();
            }
            return null;
        }


        public async Task<ResponseNoteModel> pin(long UserID,long noteID)
        {
            try
            {

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
            catch (Exception)
            {
                throw;
            }

        }



       

        public async Task<NoteReminder> SetNoteReminder(NoteReminder reminder,long UserID,long NoteID)
        {
            try
            {
               notesContext.Notes.FirstOrDefault(
                    N => N.NoteId == NoteID && N.UserId == UserID).Reminder = reminder.ReminderOn;
                NoteReminder NewNote = new NoteReminder
                {
                    UserID = (long)reminder.UserID,
                    ReminderOn = (DateTime)reminder.ReminderOn,
                    NoteID = (long)reminder.NoteID,

                };
                await notesContext.SaveChangesAsync();
                return reminder;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<string> DeleteNoteReminder(long UserID,long NoteID)
        {
            try
            {
                notesContext.Notes.FirstOrDefault(
                   N => N.NoteId == NoteID && N.UserId == UserID).Reminder=null;
                
                await notesContext.SaveChangesAsync();
                return "Note reminder delete successfully";
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<ResponseNoteModel> UpdateNote(ResponseNoteModel Note,long UserId, long NoteID)
        {
            try
            {

                var UpdateNote = notesContext.Notes.FirstOrDefault(N => N.NoteId == NoteID && N.UserId == UserId);
                if (UpdateNote != null)
                {
                  
                    UpdateNote.Title = Note.Title;
                    UpdateNote.Body = Note.Body;
                    UpdateNote.ModificaionTime = DateTime.Now;
                };
                ResponseNoteModel NewNote = new ResponseNoteModel
                {
                    UserID = (long)UpdateNote.UserId,
                    NoteId = UpdateNote.NoteId,
                    Title = UpdateNote.Title,
                    Body = UpdateNote.Body,
                    Color = UpdateNote.Color,
                    Image = UpdateNote.Image,
                    Reminder = (DateTime)UpdateNote.Reminder,
                    isPin = (bool)UpdateNote.IsPin,
                    isArchieve = (bool)UpdateNote.IsArchieve,
                    isTrash = (bool)UpdateNote.IsTrash,

                };

                await this.notesContext.SaveChangesAsync();
                return NewNote;

       
            }
            catch(Exception)
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

                    UserId = note.UserID,
                    NoteId = note.NoteId,
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
                return note;
               
            }
            catch (Exception)
            {
                throw;
            }


        }


        

        public async Task<ResponseNoteModel> ChangeColor(long UserID, string color,long noteID)
        {
            try
            {

                var note = this.notesContext.Notes.Where(n => n.NoteId == noteID && n.IsTrash == false).SingleOrDefault();
                if (note != null)
                
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
                return null;
            }
            catch(Exception)
            {
                throw;
            }
         
        }

        public async Task<ResponseNoteModel> UploadImage(IFormFile file, long UserID ,long noteID)
        {
           
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
            catch (Exception )
            {
                throw;
            }
        }










        public async Task<string> EmptyTrash()
        {
            try
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
            catch(Exception)
            {
                throw;
            }
        }


        public async Task<ResponseNoteModel> Restore(long UserID,long noteID)
        {
            try
            {
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
            catch(Exception)
            {
                throw;
            }

        }
        
    }
}

        