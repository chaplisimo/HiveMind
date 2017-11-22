import java.awt.Color;

import javax.media.j3d.AmbientLight;
import javax.media.j3d.Appearance;
import javax.media.j3d.BoundingSphere;
import javax.media.j3d.BranchGroup;
import javax.media.j3d.DirectionalLight;
import javax.media.j3d.Material;
import javax.media.j3d.Texture;
import javax.media.j3d.Texture2D;
import javax.media.j3d.TextureAttributes;
import javax.media.j3d.Transform3D;
import javax.media.j3d.TransformGroup;
import javax.vecmath.Color3f;
import javax.vecmath.Color4f;
import javax.vecmath.Point3d;
import javax.vecmath.Vector3f;

import com.sun.j3d.utils.geometry.Sphere;
import com.sun.j3d.utils.universe.SimpleUniverse;

public class SqrMapGenerator3D {
	
	public static void main(String[] args) {
		float resolution = 0.25f;
		
		//SqrMapGenerator sqrMap = new SqrMapGenerator(40,25,45,170591);
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
		for(int i=0; i<sqrMap.height; i++){
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
		}
		
		/*Sphere sphere = new Sphere(1f, appearance);
		Transform3D transform = new Transform3D();
		Vector3f vector = new Vector3f(0f, 0f, .0f);
		transform.setTranslation(vector);
		tg.setTransform(transform);
		tg.addChild(sphere);*/
		
		group.addChild(tg);
		
		// above pyramid
		Vector3f viewTranslation = new Vector3f();
		viewTranslation.z = 20f;
		viewTranslation.x = sqrMap.width * resolution/ 2;
		viewTranslation.y = -sqrMap.height * resolution/ 2;
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
		
		
		sqrMap.marchSquares();
		sqrMap.printConsole();
	}
}