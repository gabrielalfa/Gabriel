using Corrida_Bohrer_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Service;


namespace Corrida_Bohrer_2.Models
{
    public class ReceiveNotification
    {

        public void teste()
        {

            bool isSandbox = true;

            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            try
            {

                AccountCredentials credentials = PagSeguroConfiguration.Credentials(isSandbox);

                // TODO: Substitute the code below with a notification code for your transaction. 
                // You receive this notification code through a post on the URL that you specify in 
                // this page: https://pagseguro.uol.com.br/integracao/notificacao-de-transacoes.jhtml

                // Use notificationType to check if is PreApproval (preApproval or transaction)
                Transaction transaction = NotificationService.CheckTransaction(credentials, "5759A492-2799-4910-9A51-D09BF2840FCF", false);

                Console.WriteLine(transaction);
                Console.ReadKey();

                Transaction preApprovalTransaction = NotificationService.CheckTransaction(credentials, "DE30B7B698F847ED920D80791E640D15", true);

                Console.WriteLine(preApprovalTransaction);
                Console.ReadKey();
            }
            catch (PagSeguroServiceException exception)
            {
                Console.WriteLine(exception.Message + "\n");

                foreach (ServiceError element in exception.Errors)
                {
                    Console.WriteLine(element + "\n");
                }
                Console.ReadKey();
            }

        }
        
    }
}