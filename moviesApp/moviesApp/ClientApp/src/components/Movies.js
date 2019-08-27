import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../store/Movies';
import Octicon, {Pencil, Trashcan, Search} from '@primer/octicons-react'

class Movies extends Component {
  componentDidMount() {
    // This method is called when the component is first added to the document
    this.props.requestMovies();
    this.unlisten = this.props.history.listen(({ pathname }) => {
      this.props.requestMovies();
    });
  }

  componentWillUnmount() {
    this.unlisten();
  }

  render() {
    return (
      <div>
        <h1>Movies</h1>
        <p>Shows all the movies and search using a filter.</p>

        {renderSearch(this.props, this.handleKeyDown, this.handleTextChanged, this.handleSearchMovies)}
        {renderMovies(this.props, this.handleDeleteMovie, this.handleCancelDeleteMovie, this.handleConfirmDeleteMovie)}
        {renderFooter(this.props)}
      </div>
    );
  }

  handleKeyDown= (event) => {
    if (event.key === 'Enter') {
      this.props.searchMovies(event.target.value);
      this.props.history.push(`/movies?textToSearch=${event.target.value}`)
      event.preventDefault();
    }
  }

  handleTextChanged= (event) => {
    this.props.changeTextToSearch(event.target.value)
  }

  handleSearchMovies= () => {
      const textToSearch = this.props.textToSearch;
      this.props.searchMovies(textToSearch);
      this.props.history.push(`/movies?textToSearch=${textToSearch}`)
  }

  handleDeleteMovie = (movieId) => {
    this.props.markForDeletion(movieId);
    this.forceUpdate();
  }

  handleCancelDeleteMovie = (movieId) => {
    this.props.cancelDeletion(movieId);
    this.forceUpdate();
  }

  handleConfirmDeleteMovie = (movieId) => {
    this.props.deleteMovie(movieId);
    this.forceUpdate();
  }
}

function renderSearch(props, handleKeyDown, handleTextChanged, handleSearchMovies) {
  return(
    <div className="container w-75 mb-2 clearfix">
      <div className="row">
        <div className="col"></div>
        <div className="col mr-4">
          <div className="float-right mr-5 input-group">
            <input type="text" className="form-control w-25" value={props.textToSearch} onKeyDown={handleKeyDown} onChange={handleTextChanged} />
            <button type="button" className="btn btn-info ml-1" onClick={handleSearchMovies}>
              <Octicon icon={Search}/>
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

function renderMovies(props, handleDeleteMovie, handleCancelDeleteMovie, handleConfirmDeleteMovie) {
  return (
    <div className="w-75">
        {props.movies.map(movie =>

          <div className="container bg-light border-top border-bottom border-left border-right border-info ml-5 mr-5 mb-4 pl-4 " key={movie.id}>
            <div className="row">
              <div className="col">
                <img src={movie.image} className="w-100" alt="..."/>
              </div>
              <div className="col-9">
                <div className="card" style={{width: '35rem'}}>
                  <div className="card-body">
                    <h5 className="card-title">{movie.title}</h5>
                    <h6 className="card-subtitle mb-2 text-muted">Director: {movie.director}</h6>
                  <p className="card-text">Actors: {movie.actors}</p>
                  <p className="card-text">Year: {movie.year}</p>
                {movie.isMarkedForDeletion}
                  {!movie.isMarkedForDeletion &&
                  <div className="float-right">
                    <Link className='btn btn-default pull-left text-info' to={`/edit/${movie.id}`}><Octicon icon={Pencil} size='small'/></Link>
                    <a className='btn btn-default pull-left text-info' onClick={() => handleDeleteMovie(movie.id)}><Octicon icon={Trashcan} size='small'/></a>
                  </div>
                  }
                  {movie.isMarkedForDeletion === true &&
                  <div className="float-right">
                      <span className="text-info clearfix">Do you want to delete the movie?</span>
                      <div className="float-right clearfix">
                        <button type="button" className="btn btn-info mr-2" onClick={() => handleCancelDeleteMovie(movie.id)}>Cancel</button>
                        <button type="button" className="btn btn-info" onClick={() => handleConfirmDeleteMovie (movie.id)}>Delete</button>
                      </div>
                  </div>
                  }
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
