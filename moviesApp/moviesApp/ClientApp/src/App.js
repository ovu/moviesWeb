import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Movies from './components/Movies';
import NewMovie from './components/NewMovie';
import EditMovie from './components/EditMovie';

export default () => (
  <Layout>
    <Route exact path='/' component={Home} />
    <Route path='/movies' component={Movies} />
    <Route path='/new' component={NewMovie} />
    <Route path='/edit/:movieId' component={EditMovie} />
  </Layout>
);
