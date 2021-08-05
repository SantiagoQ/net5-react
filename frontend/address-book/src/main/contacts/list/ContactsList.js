import { Grid, List, ListSubheader, ListItem, ListItemText } from '@material-ui/core';
import React from 'react';
import { useSelector } from 'react-redux';
import { makeStyles } from '@material-ui/styles';

const useStyles = makeStyles(theme => ({
    listContainer: {
        overflow: "auto"
    },
    listSection: {
        backgroundColor: 'inherit',
    },
    ul: {
        backgroundColor: 'inherit',
        padding: 0,
    },
}));
function ContactsList() {
    const classes = useStyles();
    const data = useSelector(reducers => reducers.contacts);

    return <React.Fragment>
        <Grid item>
            Contacts
        </Grid>
        <Grid item>
            Search
        </Grid>
        <Grid item xs className={classes.listContainer}>
            <List>
                {data.letters.map((group) => {
                    return <li key={`group-${group}`} className={classes.listSection}>
                        <ul className={classes.ul}>
                            <ListSubheader>{group}</ListSubheader>
                            {data.list.filter((c => c.name.charAt(0).toUpperCase() === group)).map((item) => {
                                return <ListItem key={item.id}>
                                    <ListItemText primary={item.name} />
                                </ListItem>
                            })}
                        </ul>
                    </li>
                })}
            </List>
        </Grid>
    </React.Fragment>
}

export default ContactsList;