// --------------------------------------------------------------------------------------------------
// This file was automatically generated by J2CS Translator (http://j2cstranslator.sourceforge.net/). 
// Version 1.3.6.20110331_01     
// 11/05/19 19:45    
// ${CustomMessageForDisclaimer}                                                                             
// --------------------------------------------------------------------------------------------------
 namespace Jp.Maker1.Vsys3.Tools {
	

	using System;
     using System.Drawing;  
	using System.Collections;
	using System.ComponentModel;
	using System.IO;
	using System.Runtime.CompilerServices;
	
	public interface VsElement {
		String ElemType();
	
		BoundingBox BoundingBox();
	
		VsElement Clip2D(Clipper paramClipper);
	
		VsElement Clip3DF(Clipper paramClipper);
	
		double Depth();
	
		void Draw(Graphics paramGraphics);
	
		void Complete_draw(Graphics paramGraphics);
	
		void Print();
	
		VsElement Project(Projector paramProjector);
	
		VsElement Transform(Matrix44 paramMatrix44);
	
		void SetColor(Color paramColor);
	
		void SetMaterial(Material paramMaterial);
	}}
