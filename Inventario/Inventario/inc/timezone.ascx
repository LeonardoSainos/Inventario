

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="timezone.ascx.cs" Inherits="Inventario.Inventario.inc.timezone" %>
 

<%
    DateTime now = DateTime.Now;
    TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
    DateTime mexicoCityTime = TimeZoneInfo.ConvertTime(now, timeZone);
    string fecha = mexicoCityTime.ToString("dddd d 'de' MMMM 'del' yyyy");
    Response.Write(fecha);
%>