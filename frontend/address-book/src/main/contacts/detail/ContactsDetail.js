import { Button, Grid } from '@material-ui/core';
import React from 'react';
import { useSelector } from 'react-redux';
import { makeStyles } from '@material-ui/styles';

const useStyles = makeStyles(theme => ({
    actionContainer: {
        // padding: "0px 10px",
    },
    action: {
        padding: "0px",
    }
}));
function ContactsDetail() {
    const classes = useStyles();
    const data = useSelector(reducers => reducers.contacts);

    return <React.Fragment>
        <Grid item container className={classes.actionContainer} justifyContent="space-between" alignItems="center">
            <Grid item><Button className={classes.action}>Edit</Button></Grid>
            <Grid item><Button className={classes.action}>Delete</Button></Grid>
        </Grid>
        <Grid item xs>
            Title
        </Grid>
        <Grid item xs>
            DEtail
        </Grid>
    </React.Fragment >
}

export default ContactsDetail;