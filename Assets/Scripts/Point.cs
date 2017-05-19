
public struct Point {

	public int x;
	public int y;

	public Point(int x,int y){
		this.x = x;
		this.y = y;
	}

	public static readonly int[] Cos = { 1, 0, -1, 0 };
	public static readonly int[] Sin = { 0, 1, 0, -1 };
 
	// 原点から、指定した方向に1歩進んだ位置座標
	public static Point Delta (Direction dir)
	{
    	return new Point (Cos [(int)dir], Sin [(int)dir]);
	}

	public static Point operator+ (Point left,Point right){
		return new Point(left.x+right.x,left.y+right.y);
	}

//	public override bool Equals(System.Object obj)
//    {
//        // If parameter is null return false.
//        if (obj == null)
//        {
//            return false;
//        }
//
//        // If parameter cannot be cast to Point return false.
//        Point p = obj as Point;
//        if ((System.Object)p == null)
//        {
//            return false;
//        }
//
//        // Return true if the fields match:
//        return (x == p.x) && (y == p.y);
//    }

	public bool Equals(Point p)
    {
        // If parameter is null return false:
        if ((object)p == null)
        {
            return false;
        }

        // Return true if the fields match:
        return (x == p.x) && (y == p.y);
    }

	public override int GetHashCode()
    {
        return x ^ y;
    }

}

public enum Direction{


	Right,Up,Left,Down

}
