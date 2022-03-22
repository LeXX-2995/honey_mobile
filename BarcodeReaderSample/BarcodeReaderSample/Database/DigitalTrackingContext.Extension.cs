using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TraceIQ.Expeditor.Models;

namespace BarcodeReaderSample.Database
{
    public partial class DigitalTrackingContext
    {
        public static OperationResult Run(Func<DigitalTrackingContext, OperationResult> action)
        {
            using var db = new DigitalTrackingContext();
            try
            {
                return action(db);
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                    Debugger.Break();

                return OperationResult.Fail(ex.Message);
            }
        }

        public static OperationResult<T> Run<T>(Func<DigitalTrackingContext, OperationResult<T>> action)
        {
            using var db = new DigitalTrackingContext();
            try
            {
                return action(db);
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                    Debugger.Break();

                return OperationResult<T>.Fail(ex.Message);
            }
        }
    }
}
