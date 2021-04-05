using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.NoteException
{
    public class NoteException : Exception
    {
        public enum ExceptionType
        {
            NOTES_NOT_EXIST,
            WRONG_NOTEID,
            NO_COLORCODE,
            NO_IMAGE
        }
        public ExceptionType exceptionType;
        public NoteException(ExceptionType exceptionType, string message) : base(message)
        {
            this.exceptionType = exceptionType;
        }
    }
}

