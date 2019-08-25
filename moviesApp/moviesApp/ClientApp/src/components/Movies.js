import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../store/Movies';
import Octicon, {Pencil, Trashcan} from '@primer/octicons-react'

class Movies extends Component {
  componentDidMount() {
    // This method is called when the component is first added to the document
    this.props.requestMovies();
  }

  componentDidUpdate() {
    // This method is called when the route parameters change
    //this.props.requestMovies();
  }

  render() {
    return (
      <div>
        <h1>Movies</h1>
        <p>Shows all the movies and search using a filter.</p>


        {renderMovies(this.props)}
        {renderFooter(this.props)}
      </div>
    );
  }
}

function renderMovies(props) {
  return (
    <div className="w-75">
        {props.movies.map(movie =>

          <div class="container bg-light border-top border-bottom border-info ml-5 mr-5 mb-4 pl-4 ">
            <div class="row">
              <div class="col">
                <img src={movie.image} className="w-100" alt="..."/>
              </div>
              <div class="col-9">
                <div className="card" style={{width: '35rem'}}>
                  <div className="card-body">
                    <h5 class="card-title">{movie.title}</h5>
                    <h6 class="card-subtitle mb-2 text-muted">Director: {movie.director}</h6>
                  <p className="card-text">Actors: {movie.actors}</p>
                  <p className="card-text">Year: {movie.year}</p>
                  <div className="float-right">
                    <Link className='btn btn-default pull-left' to={`/edit`}><Octicon icon={Pencil}/></Link>
                    <Link className='btn btn-default pull-left' to={`/delete`}><Octicon icon={Trashcan}/></Link>
                  </div>
                  </div>
                </div>
              </div>
            </div>
          </div>

        )}
    </div>
  );
}

function renderFooter(props) {
  return <p className='clearfix text-center'>
    {props.isLoading ? <span>Loading...</span> : []}
  {props.isLoading}
  </p>;
}

export default connect(
  state => state.moviesReducer,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Movies);
