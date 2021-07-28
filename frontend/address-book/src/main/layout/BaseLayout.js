import React from 'react';
import { makeStyles } from '@material-ui/styles';
import { Container } from '@material-ui/core';

const useStyles = makeStyles(theme => ({
    root: {
        background: theme.palette.secondary.main,
    }
}));
function BaseLayout() {
    const classes = useStyles();
    return (
        <Container className={classes.root}>
            <p>Test</p>
        </Container>
    )
}

export default BaseLayout;