import { Card, CardContent, Grid, Typography, CardMedia, Button } from '@mui/material'
import React, { useState } from 'react'
import { useRouter } from 'next/router';

export const CatalogueDetailsView = ({ productDescription, image, points , handleGetProduct}) => {
  const [isImageOpen, setIsImageOpen] = useState(false);
  const router = useRouter();

  const handleImageClick = () => {
    setIsImageOpen(!isImageOpen);
  }

  const handleBackClick = () => {
    router.back();
  }

  return (
    <Card sx={{ width: '90%', margin: 'auto', mt: 3, boxShadow: '0px 0px 15px rgba(0, 0, 0, 0.2)' }}>
      <Grid container>
        <Grid item xs={12} md={6}>
          <CardMedia
            component="img"
            height="100%"
            image={image}
            alt={productDescription}
            onClick={handleImageClick}
            sx={{
              objectFit: 'contain',
              borderRadius: '5px',
              cursor: 'pointer',
              transition: 'transform 0.3s ease',
              ...(isImageOpen && {
                position: 'fixed',
                top: '50%',
                left: '50%',
                transform: 'translate(-50%, -50%)',
                width: '80%',
                maxHeight: '80%',
                zIndex: '100',
                objectFit: 'contain',
              })
            }}
          />
        </Grid>
        <Grid item xs={12} md={6}>
          <CardContent sx={{ height: '100%' }}>
            <Typography variant="h4" sx={{ fontSize: 38, fontWeight: 'bold', mb: 2 }}>
              {productDescription}
            </Typography>
            <Typography variant="subtitle1" sx={{ fontSize: 20, fontWeight: 'bold', mb: 1 }}>
              Points:
            </Typography>
            <Typography variant="body1" sx={{ fontSize: 18, marginBottom: 3 }}>
              {points}
            </Typography>
            <Typography variant="subtitle1" sx={{ fontSize: 20, fontWeight: 'bold', mb: 1 }}>
              Description:
            </Typography>
            <Typography variant="body1" sx={{ fontSize: 18, marginBottom: 3 }}>
              {productDescription}
            </Typography>
            <Button
              variant="contained"
              color="primary"
              size="large"
              sx={{ mt: 2 }}
              onClick={() =>handleGetProduct(points)}
            >
              Buy now
            </Button>
            <Button
              variant="contained"
              color="secondary"
              size="large"
              sx={{ mt: 2, ml: 2 }}
              onClick={handleBackClick}
            >
              Back
            </Button>
          </CardContent>
        </Grid>
      </Grid>
      {isImageOpen && (
        <Button
          variant="contained"
          color="primary"
          size="large"
          sx={{ position: 'fixed', bottom: '2rem', right: '2rem', zIndex: '200' }}
          onClick={handleImageClick}
        >
          Close
        </Button>
      )}
    </Card>
  );
};
