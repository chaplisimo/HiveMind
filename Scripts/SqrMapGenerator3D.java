import java.awt.Color;

import javax.media.j3d.AmbientLight;
import javax.media.j3d.Appearance;
import javax.media.j3d.BoundingSphere;
import javax.media.j3d.BranchGroup;
import javax.media.j3d.DirectionalLight;
import javax.media.j3d.GeometryArray;
import javax.media.j3d.Material;
import javax.media.j3d.Shape3D;
import javax.media.j3d.Texture;
import javax.media.j3d.Texture2D;
import javax.media.j3d.TextureAttributes;
import javax.media.j3d.Transform3D;
import javax.media.j3d.TransformGroup;
import javax.media.j3d.TriangleArray;
import javax.vecmath.Color3f;
import javax.vecmath.Color4f;
import javax.vecmath.Point3d;
import javax.vecmath.Point3f;
import javax.vecmath.Vector3f;

import com.sun.j3d.utils.geometry.GeometryInfo;
import com.sun.j3d.utils.geometry.NormalGenerator;
import com.sun.j3d.utils.geometry.Sphere;
import com.sun.j3d.utils.universe.SimpleUniverse;

public class SqrMapGenerator3D {
	
	public static void main(String[] args) {
		float resolution = 0.25f;
		
		//SqrMapGenerator sqrMap = new SqrMapGenerator(10,10,20,170591);
		SqrMapGenerator sqrMap = new SqrMapGenerator();
		sqrMap.init();
		sqrMap.smooth();
		
		SimpleUniverse universe = new SimpleUniverse();
		BranchGroup group = new BranchGroup();
		
		//Point3f e = new Point3f(1.0f, 0.0f, 0.0f);
		Appearance appearance = new Appearance();
		Color3f color = new Color3f(Color.yellow);
		Color3f black = new Color3f(0.0f, 0.0f, 0.0f);
		Color3f white = new Color3f(1.0f, 1.0f, 1.0f);
		Texture texture = new Texture2D();
		TextureAttributes texAttr = new TextureAttributes();
		texAttr.setTextureMode(TextureAttributes.MODULATE);
		texture.setBoundaryModeS(Texture.WRAP);
		texture.setBoundaryModeT(Texture.WRAP);
		texture.setBoundaryColor(new Color4f(0.0f, 1.0f, 0.0f, 0.0f));
		Material mat = new Material(color, black, color, white, 70f);
		appearance.setTextureAttributes(texAttr);
		appearance.setMaterial(mat);
		appearance.setTexture(texture);
		
		TransformGroup tg = new TransformGroup();
		/*for(int i=0; i<sqrMap.height; i++){
			for(int j=0; j<sqrMap.width; j++){
				if(sqrMap.map[i][j]==1){
					TransformGroup tg2 = new TransformGroup();
					Sphere sphere = new Sphere(0.1f, appearance);
					Transform3D transform = new Transform3D();
					Vector3f vector = new Vector3f(resolution * j,.0f, resolution * i);
					transform.setTranslation(vector);
					tg2.setTransform(transform);
					tg2.addChild(sphere);
					tg.addChild(tg2);
				}
			}
		}*/
		
		sqrMap.marchSquares();
		sqrMap.printSqrmap();
		
		for(int i=0; i<sqrMap.height-1; i++){
			for(int j=0; j<sqrMap.width-1; j++){
				if(sqrMap.msqrs[i][j]!=0){
					TransformGroup tg2 = getMarchingSquare(sqrMap.msqrs[i][j]);
					Transform3D transform = new Transform3D();
					Vector3f vector = new Vector3f(j,.0f,i);
					transform.setTranslation(vector);
					tg2.setTransform(transform);
					tg.addChild(tg2);
				}
			}
		}
		
		group.addChild(tg);
		
		
		// above pyramid
		Vector3f viewTranslation = new Vector3f();
		viewTranslation.z = 50f;
		viewTranslation.x = sqrMap.width / 2;
		viewTranslation.y = -sqrMap.height / 2;
		Transform3D viewTransform = new Transform3D();
		viewTransform.setTranslation(viewTranslation);
		Transform3D rotation = new Transform3D();
		rotation.rotX(-Math.PI / 2.0d);
		rotation.mul(viewTransform);
		universe.getViewingPlatform().getViewPlatformTransform().setTransform(
				rotation);
		universe.getViewingPlatform().getViewPlatformTransform().getTransform(
				viewTransform);
		
		// lights
		BoundingSphere bounds = new BoundingSphere(new Point3d(0.0, 0.0, 0.0),
				1000.0);
		Color3f light1Color = new Color3f(.7f, .7f, .7f);
		Vector3f light1Direction = new Vector3f(4.0f, -7.0f, -12.0f);
		DirectionalLight light1 = new DirectionalLight(light1Color, light1Direction);
		light1.setInfluencingBounds(bounds);
		group.addChild(light1);
		Color3f ambientColor = new Color3f(.4f, .4f, .4f);
		AmbientLight ambientLightNode = new AmbientLight(ambientColor);
		ambientLightNode.setInfluencingBounds(bounds);
		group.addChild(ambientLightNode);
		
		universe.addBranchGraph(group);
		
	}
	
	public static class MarchPoints {
		public static final Point3f topLeft = new Point3f(0f, 0f, 1f);
		public static final Point3f topCenter = new Point3f(0.5f, 0f, 1f);
		public static final Point3f topRight = new Point3f(1f, 0f, 1f);
		       
		public static final Point3f middleLeft = new Point3f(0f, 0f, .5f);
		public static final Point3f middleRight = new Point3f(1f, 0f, .5f);
		        
		public static final Point3f downLeft = new Point3f(0f, 0f, 0f);    
		public static final Point3f downCenter = new Point3f(0.5f, 0f, 0f);
		public static final Point3f downRight = new Point3f(1f, 0f, 0f);   
		
	}
	
	static TransformGroup getMarchingSquare(int binary){
		TransformGroup tg = new TransformGroup();
		TriangleArray sqrGeometry = null;
		switch (binary) {
		case 0 : {
			sqrGeometry = new TriangleArray(0,TriangleArray.COORDINATES);
			break;
			}
		case 1 : {
			sqrGeometry = new TriangleArray(3,TriangleArray.COORDINATES);
			sqrGeometry.setCoordinate(0, MarchPoints.middleLeft);
			sqrGeometry.setCoordinate(1, MarchPoints.downCenter);
			sqrGeometry.setCoordinate(2, MarchPoints.downLeft);
			break;
			}
		case 2 : {
			sqrGeometry = new TriangleArray(3,TriangleArray.COORDINATES);
			sqrGeometry.setCoordinate(0, MarchPoints.middleRight);
			sqrGeometry.setCoordinate(1, MarchPoints.downRight);
			sqrGeometry.setCoordinate(2, MarchPoints.downCenter);
			break;
			}
		case 3 : {
			sqrGeometry = new TriangleArray(6,TriangleArray.COORDINATES);
			sqrGeometry.setCoordinate(0, MarchPoints.middleLeft);
			sqrGeometry.setCoordinate(1, MarchPoints.middleRight);
			sqrGeometry.setCoordinate(2, MarchPoints.downRight);
			
			sqrGeometry.setCoordinate(3, MarchPoints.middleLeft);
			sqrGeometry.setCoordinate(4, MarchPoints.downRight);
			sqrGeometry.setCoordinate(5, MarchPoints.downLeft);
			break;
			}
		case 4 : {
			sqrGeometry = new TriangleArray(3,TriangleArray.COORDINATES);
			sqrGeometry.setCoordinate(0, MarchPoints.topCenter);
			sqrGeometry.setCoordinate(1, MarchPoints.topRight);
			sqrGeometry.setCoordinate(2, MarchPoints.middleRight);
			break;
			}
		case 5 : {
			sqrGeometry = new TriangleArray(12,TriangleArray.COORDINATES);
			sqrGeometry.setCoordinate(0, MarchPoints.topCenter);
			sqrGeometry.setCoordinate(1, MarchPoints.topRight);
			sqrGeometry.setCoordinate(2, MarchPoints.middleRight);
			
			sqrGeometry.setCoordinate(3, MarchPoints.topCenter);
			sqrGeometry.setCoordinate(4, MarchPoints.middleRight);
			sqrGeometry.setCoordinate(5, MarchPoints.downCenter);
			
			sqrGeometry.setCoordinate(6, MarchPoints.topCenter);
			sqrGeometry.setCoordinate(7, MarchPoints.downCenter);
			sqrGeometry.setCoordinate(8, MarchPoints.downLeft);
			
			sqrGeometry.setCoordinate(9, MarchPoints.topCenter);
			sqrGeometry.setCoordinate(10, MarchPoints.downLeft);
			sqrGeometry.setCoordinate(11, MarchPoints.middleLeft);
			break;
			}
		case 6 : {
			sqrGeometry = new TriangleArray(6,TriangleArray.COORDINATES);
			sqrGeometry.setCoordinate(0, MarchPoints.topCenter);
			sqrGeometry.setCoordinate(1, MarchPoints.topRight);
			sqrGeometry.setCoordinate(2, MarchPoints.downRight);
			
			sqrGeometry.setCoordinate(3, MarchPoints.topCenter);
			sqrGeometry.setCoordinate(4, MarchPoints.downRight);
			sqrGeometry.setCoordinate(5, MarchPoints.downCenter);
			break;
			}
		case 7 : {
			sqrGeometry = new TriangleArray(9,TriangleArray.COORDINATES);
			sqrGeometry.setCoordinate(0, MarchPoints.topCenter);
			sqrGeometry.setCoordinate(1, MarchPoints.topRight);
			sqrGeometry.setCoordinate(2, MarchPoints.downRight);
			
			sqrGeometry.setCoordinate(3, MarchPoints.topCenter);
			sqrGeometry.setCoordinate(4, MarchPoints.downRight);
			sqrGeometry.setCoordinate(5, MarchPoints.downLeft);
			
			sqrGeometry.setCoordinate(6, MarchPoints.topCenter);
			sqrGeometry.setCoordinate(7, MarchPoints.downLeft);
			sqrGeometry.setCoordinate(8, MarchPoints.middleLeft);
			break;
			}
		case 8 : {
			sqrGeometry = new TriangleArray(3,TriangleArray.COORDINATES);
			sqrGeometry.setCoordinate(0, MarchPoints.topLeft);
			sqrGeometry.setCoordinate(1, MarchPoints.topCenter);
			sqrGeometry.setCoordinate(2, MarchPoints.middleLeft);
			break;
			}
		case 9 : {
			sqrGeometry = new TriangleArray(6,TriangleArray.COORDINATES);
			sqrGeometry.setCoordinate(0, MarchPoints.topLeft);
			sqrGeometry.setCoordinate(1, MarchPoints.topCenter);
			sqrGeometry.setCoordinate(2, MarchPoints.downCenter);
			
			sqrGeometry.setCoordinate(3, MarchPoints.topLeft);
			sqrGeometry.setCoordinate(4, MarchPoints.downCenter);
			sqrGeometry.setCoordinate(5, MarchPoints.downLeft);
			break;
			}
		case 10 : {
			sqrGeometry = new TriangleArray(9,TriangleArray.COORDINATES);
			sqrGeometry.setCoordinate(0, MarchPoints.topLeft);
			sqrGeometry.setCoordinate(1, MarchPoints.topCenter);
			sqrGeometry.setCoordinate(2, MarchPoints.middleRight);
			
			sqrGeometry.setCoordinate(3, MarchPoints.topLeft);
			sqrGeometry.setCoordinate(4, MarchPoints.middleRight);
			sqrGeometry.setCoordinate(5, MarchPoints.downRight);
			//
			sqrGeometry.setCoordinate(6, MarchPoints.topLeft);
			sqrGeometry.setCoordinate(7, MarchPoints.downRight);
			sqrGeometry.setCoordinate(8, MarchPoints.downLeft);
			
			sqrGeometry.setCoordinate(9, MarchPoints.topLeft);
			sqrGeometry.setCoordinate(10, MarchPoints.downLeft);
			sqrGeometry.setCoordinate(11, MarchPoints.middleLeft);
			break;
			}
		case 11 : {
			sqrGeometry = new TriangleArray(9,TriangleArray.COORDINATES);
			sqrGeometry.setCoordinate(0, MarchPoints.topLeft);
			sqrGeometry.setCoordinate(1, MarchPoints.topCenter);
			sqrGeometry.setCoordinate(2, MarchPoints.middleRight);
			
			sqrGeometry.setCoordinate(3, MarchPoints.topLeft);
			sqrGeometry.setCoordinate(4, MarchPoints.middleRight);
			sqrGeometry.setCoordinate(5, MarchPoints.downRight);
			
			sqrGeometry.setCoordinate(6, MarchPoints.topLeft);
			sqrGeometry.setCoordinate(7, MarchPoints.downRight);
			sqrGeometry.setCoordinate(8, MarchPoints.downLeft);
			break;
			}
		case 12 : {
			sqrGeometry = new TriangleArray(6,TriangleArray.COORDINATES);
			sqrGeometry.setCoordinate(0, MarchPoints.topLeft);
			sqrGeometry.setCoordinate(1, MarchPoints.topRight);
			sqrGeometry.setCoordinate(2, MarchPoints.middleRight);
			
			sqrGeometry.setCoordinate(3, MarchPoints.topLeft);
			sqrGeometry.setCoordinate(4, MarchPoints.middleRight);
			sqrGeometry.setCoordinate(5, MarchPoints.middleLeft);
			break;
			}
		case 13 : {
			sqrGeometry = new TriangleArray(9,TriangleArray.COORDINATES);
			sqrGeometry.setCoordinate(0, MarchPoints.topLeft);
			sqrGeometry.setCoordinate(1, MarchPoints.topRight);
			sqrGeometry.setCoordinate(2, MarchPoints.middleRight);
			
			sqrGeometry.setCoordinate(3, MarchPoints.topLeft);
			sqrGeometry.setCoordinate(4, MarchPoints.middleRight);
			sqrGeometry.setCoordinate(5, MarchPoints.downCenter);
			
			sqrGeometry.setCoordinate(6, MarchPoints.topLeft);
			sqrGeometry.setCoordinate(7, MarchPoints.downCenter);
			sqrGeometry.setCoordinate(8, MarchPoints.downLeft);
			break;
			}
		case 14 : {
			sqrGeometry = new TriangleArray(9,TriangleArray.COORDINATES);
			sqrGeometry.setCoordinate(0, MarchPoints.topLeft);
			sqrGeometry.setCoordinate(1, MarchPoints.topRight);
			sqrGeometry.setCoordinate(2, MarchPoints.downRight);
			
			sqrGeometry.setCoordinate(3, MarchPoints.topLeft);
			sqrGeometry.setCoordinate(4, MarchPoints.downRight);
			sqrGeometry.setCoordinate(5, MarchPoints.downCenter);
			
			sqrGeometry.setCoordinate(6, MarchPoints.topLeft);
			sqrGeometry.setCoordinate(7, MarchPoints.downCenter);
			sqrGeometry.setCoordinate(8, MarchPoints.middleLeft);
			break;
			}
		case 15 : {
			sqrGeometry = new TriangleArray(6,TriangleArray.COORDINATES);
			sqrGeometry.setCoordinate(0, MarchPoints.topLeft);
			sqrGeometry.setCoordinate(1, MarchPoints.topRight);
			sqrGeometry.setCoordinate(2, MarchPoints.downRight);
			
			sqrGeometry.setCoordinate(3, MarchPoints.topLeft);
			sqrGeometry.setCoordinate(4, MarchPoints.downRight);
			sqrGeometry.setCoordinate(5, MarchPoints.downLeft);
			break;
			}
		}
		GeometryInfo geometryInfo = new GeometryInfo(sqrGeometry);
		NormalGenerator ng = new NormalGenerator();
		ng.generateNormals(geometryInfo);

		GeometryArray result = geometryInfo.getGeometryArray();
		Shape3D shape = new Shape3D(result);
		tg.addChild(shape);
		
		return tg;
	}
}
