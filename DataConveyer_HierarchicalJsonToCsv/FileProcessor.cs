// Copyright © 2019-2020 Mavidian Technologies Limited Liability Company. All Rights Reserved.

using Mavidian.DataConveyer.Common;
using Mavidian.DataConveyer.Logging;
using Mavidian.DataConveyer.Orchestrators;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataConveyer_HierarchicalJsonToCsv
{
   /// <summary>
   /// Represents Data Conveyer functionality specific to processing hierarchical JSON data
   /// </summary>
   internal class FileProcessor
   {
      private readonly IOrchestrator Orchestrator;

      internal FileProcessor(string inFile, string outLocation)
      {
         var inputIsCsv = Path.GetExtension(inFile).ToLower() == ".csv";
         var outFileWithoutExtension = outLocation + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(inFile);
         var config = new OrchestratorConfig(LoggerCreator.CreateLogger(LoggerType.LogFile, "Unbound JSON translation to/from CSV.", LogEntrySeverity.Information))
         {
            ReportProgress = true,
            ProgressInterval = 1000,
            ProgressChangedHandler = (s, e) => { if (e.Phase == Phase.Intake) Console.Write($"\rProcessed {e.RecCnt:N0} records so far..."); },
            PhaseFinishedHandler = (s, e) => { if (e.Phase == Phase.Intake) Console.WriteLine($"\rProcessed {e.RecCnt:N0} records. Done!   "); },           
            InputFileName = inFile
         };

         if (inputIsCsv)  // CSV to UnboundJSON
         {
            config.InputDataKind = KindOfTextData.Delimited;
            config.HeadersInFirstInputRow = true;
            config.AllowTransformToAlterFields = true;
            config.OutputDataKind = KindOfTextData.UnboundJSON;
            config.XmlJsonOutputSettings = "IndentChars|  ";  // pretty print
            config.OutputFileName = outFileWithoutExtension + ".json";
        }
         else  // UnboundJSON to CSV
         {
            config.InputDataKind = KindOfTextData.UnboundJSON;
            config.AllowOnTheFlyInputFields = true;  // TODO: consider UnboundJSON ignoring this setting like X12 
            config.AllowTransformToAlterFields = true;  //IMPORTANT! otherwise null items will be produced!
            config.OutputDataKind = KindOfTextData.Delimited;
            config.OutputFileName = outFileWithoutExtension + ".csv";
            config.HeadersInFirstOutputRow = true;
         }

         Orchestrator = OrchestratorCreator.GetEtlOrchestrator(config);
      }

      /// <summary>
      /// Execute Data Conveyer process.
      /// </summary>
      /// <returns>Task containing the process results.</returns>
      internal async Task<ProcessResult> ProcessFileAsync()
      {
         var result = await Orchestrator.ExecuteAsync();
         Orchestrator.Dispose();

         return result;
      }

   }
}
