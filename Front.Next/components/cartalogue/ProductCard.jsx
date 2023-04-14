import * as React from 'react';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Typography from '@mui/material/Typography';
import { CardActionArea } from '@mui/material';
import Link from 'next/link';

export function ProductCard({product}) {
  return (
    <Card sx={{ width: 345 }}>
        <Link href={`catalogue/${product.id}`} style={{textDecoration:"none", color:"#111"}} >
      <CardActionArea>
        <CardMedia
          component="img"
          height="140"
          image={product.image}
          alt="product photo"
        />
        <CardContent>
          <Typography gutterBottom variant="h5" component="div">
            {product.productDescription}
          </Typography>
          <Typography variant="body1" color="text.secondary">
            Price: {product.points} Points
          </Typography>
        </CardContent>
      </CardActionArea>
      </Link>
    </Card>
  );
}