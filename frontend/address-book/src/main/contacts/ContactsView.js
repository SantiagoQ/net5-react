import { Grid } from '@material-ui/core';
import ContactsList from "./list/ContactsList";
import { makeStyles } from '@material-ui/styles';
import ContactsDetail from "./detail/ContactsDetail";

const useStyles = makeStyles(theme => ({
    container: {
        minHeight: "600px",
    }
}));
function ContactsView() {
    const classes = useStyles();

    return <Grid container direction="row" className={classes.container} >
        <Grid item container direction="column" md={5}>
            <ContactsList />
        </Grid>
        <Grid item container direction="column" md={7}>
            <ContactsDetail />
        </Grid>
    </Grid>
}

export default ContactsView;