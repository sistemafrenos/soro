using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HK.Clases
{
    class cCustom
    {
        public static DevExpress.XtraGrid.Columns.GridColumn CrearColumna(string nombre, string titulo, string campo, int ancho, bool IsEditable, bool IsFocus, bool IsNumeric, bool IsFixed)
        {
            DevExpress.XtraGrid.Columns.GridColumn retorno = new DevExpress.XtraGrid.Columns.GridColumn();
            retorno.Name = nombre;
            retorno.Caption = titulo;
            retorno.FieldName = campo;
            retorno.OptionsColumn.AllowEdit = IsEditable;
            retorno.OptionsColumn.AllowFocus = IsFocus;
            retorno.OptionsColumn.FixedWidth = IsFixed;
            retorno.Width = ancho;            
            if (IsNumeric)
            {
                DevExpress.Utils.FormatInfo formato = new DevExpress.Utils.FormatInfo();
                retorno.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                formato.FormatString = "n2";
                formato.FormatType = DevExpress.Utils.FormatType.Numeric;
                retorno.DisplayFormat.Assign(formato);
            }
            return retorno;
        }
     
    }
}
