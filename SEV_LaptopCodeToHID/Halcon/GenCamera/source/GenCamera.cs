/*****************************************************************************
 * File generated by HDevelop Version 18.11
 *
 * Do not modify!
 *****************************************************************************/

using System;
using System.IO;
using HalconDotNet;


/*
 * If you use this class in your program, you have to 
 * link against hdevenginedotnet.dll and halcondotnet.dll.
 * The Dlls are located in ${HALCONROOT}/bin/dotnet[20|35].
 *
 * The wrapped .hdev or .hdpl files have to be located in the folder
 * that is specified in the static ResourcePath property of 
 * GenCamera. 
 * By default, ResourcePath is ${binary_dir}/res_GenCamera.
 *
 * It is recommended to compile an assembly from this file using
 * the generated CMakeLists.txt.
 */

namespace HalconProcs
{
  public static class GenCamera
  {

    public static void Init_Acquisition(
        out HTuple AcqHandle)
    {     
      AddResourcePathToProcedurePath();
      using (HDevProcedureCall call = _Init_Acquisition.Value.CreateCall())
      {
        call.Execute();
        AcqHandle = GetParameterHTuple(call,"AcqHandle");
      }
    }


    /****************************************************************************
    * ResourcePath
    *****************************************************************************
    * Use ResourcePath in your application to specify the location of the 
    * HDevelop script or procedure library.
    *****************************************************************************/
    public static string ResourcePath
    {
      get
      {
        return _resource_path;
      }
      set
      {
        lock(_procedure_path_lock)
        {
          _procedure_path_initialized = false;
        }
        _resource_path = value;
      }
    }

#region Implementation details

    /* Implementation details of the wrapper class.
     * You do not have to use these functions ever.
     */

    private static bool _procedure_path_initialized = false;
    private static object _procedure_path_lock = new object();

        
    private static string _resource_path = System.AppDomain.CurrentDomain.BaseDirectory + @"Halcon\GenCamera\res_GenCamera";

    private static Lazy<HDevProgram> _Program
            = new Lazy<HDevProgram>(() => new HDevProgram(Path.Combine(GenCamera.ResourcePath, "SEV_2Barcode.hdev")));
    private static Lazy<HDevProcedure> _Init_Acquisition
            = new Lazy<HDevProcedure>(() => new HDevProcedure(_Program.Value, "Init_Acquisition"));
        
    private static HTuple GetParameterHTuple(HDevProcedureCall call, string name)
    {
      return call.GetOutputCtrlParamTuple(name);
    }

    private static HObject GetParameterHObject(HDevProcedureCall call, string name)
    {
      return call.GetOutputIconicParamObject(name);
    }

    private static HTupleVector GetParameterHTupleVector(HDevProcedureCall call, string name)
    {
      return call.GetOutputCtrlParamVector(name);
    }

    private static HObjectVector GetParameterHObjectVector(HDevProcedureCall call, string name)
    {
      return call.GetOutputIconicParamVector(name);
    }

    private static void SetParameter(HDevProcedureCall call, string name, HTuple tuple)
    {
      call.SetInputCtrlParamTuple(name,tuple);
    }

    private static void SetParameter(HDevProcedureCall call, string name, HObject obj)
    {
      call.SetInputIconicParamObject(name,obj);
    }

    private static void SetParameter(HDevProcedureCall call, string name, HTupleVector vector)
    {
      call.SetInputCtrlParamVector(name,vector);
    }

    private static void SetParameter(HDevProcedureCall call, string name, HObjectVector vector)
    {
      call.SetInputIconicParamVector(name,vector);
    }

    private static void AddResourcePathToProcedurePath() 
    {
      lock(_procedure_path_lock)
      {
        if(!_procedure_path_initialized)
        {
          new HDevEngine().AddProcedurePath(ResourcePath);
          _procedure_path_initialized = true;
        }
      }
    }

#endregion

}
}