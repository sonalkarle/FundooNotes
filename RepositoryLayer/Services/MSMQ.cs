﻿using CommanLayer.ResponseModel;
using CommonLayer.ResponseModel;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class MSMQ
    {
        Email email;
        MessageQueue queue = new MessageQueue(@".\private$\tokenQueue");

        public MSMQ(IConfiguration config)
        {
            email = new Email(config);
        }


        /// <summary>
        /// Sends the password reset link to MSMQ 
        /// </summary>
        /// <param name="link"></param>
        public void MSMQSender(forgetclass link)
        {
            try
            {
                if (!MessageQueue.Exists(queue.Path))
                {
                    MessageQueue.Create(queue.Path);
                }
                queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                Message msg = new Message
                {
                    Label = "password reset link",
                    Body = JsonConvert.SerializeObject(link),
                };
                queue.Send(msg);
                queue.ReceiveCompleted += MSMQReceiver;
                queue.BeginReceive(TimeSpan.FromSeconds(5));
                queue.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Handles the ReceiveCompleted event of the Queue control.
        /// sends email when message received from queue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MSMQReceiver(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                forgetclass model = JsonConvert.DeserializeObject<forgetclass>(msg.Body.ToString());

                email.EmailService(model);

                queue.BeginReceive(TimeSpan.FromSeconds(5));
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
