import React, { useContext } from 'react';
import { makeStyles } from '@material-ui/styles';
import { Container, Paper } from '@material-ui/core';
import { renderRoutes } from 'react-router-config';
import AppContext from '../../auxiliaries/AppContext';

const useStyles = makeStyles(theme => ({
    root: {
        background: theme.palette.secondary.main,
        minHeight: '100vh',
        overflow: 'hidden',
        padding: "25vh 35vh"
    },
    container: {
        background: theme.white,
    }
}));
function BaseLayout() {
    const classes = useStyles();
    const appContext = useContext(AppContext);
    const { routes } = appContext;
    return (
        <Container maxWidth={false} className={classes.root}>
            {/* TODO - BLOCK UI */}
            <Paper className={classes.container} elevation={3}>
                {renderRoutes(routes)}
            </Paper>
        </Container>
    )
}

export default BaseLayout;