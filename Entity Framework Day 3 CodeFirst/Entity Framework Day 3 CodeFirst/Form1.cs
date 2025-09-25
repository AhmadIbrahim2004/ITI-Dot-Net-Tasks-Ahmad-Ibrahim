using System.Windows.Forms;
using Entity_Framework_Day_3_CodeFirst.Models;       
using Entity_Framework_Day_3_CodeFirst.Repositories;

namespace Entity_Framework_Day_3_CodeFirst
{
    public partial class Form1 : Form
    {
        private readonly TheatreContext _context;
        private readonly IMovieRepository _movieRepository;
        private Movie? _selectedMovie = null;
        public Form1()
        {
            InitializeComponent();
            _context = new TheatreContext();
            _movieRepository = new MovieRepository(_context);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadMovies();
            UpdateUIState();
        }

        private void dgvMovies_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMovies.SelectedRows.Count > 0)
            {
                int selectedMovieId = (int)dgvMovies.SelectedRows[0].Cells[0].Value;
                _selectedMovie = _movieRepository.GetById(selectedMovieId);

                PopulateInputs();
            }
            else
            {
                _selectedMovie = null;
            }
            UpdateUIState();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Title cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var newMovie = new Movie
            {
                Title = txtTitle.Text,
                DurationMin = (short)numDuration.Value,
                IsActive = chkIsActive.Checked ? "Y" : "N"
            };

            _movieRepository.Add(newMovie);

            LoadMovies();
            ClearInputs();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedMovie == null) return; 

            _selectedMovie.Title = txtTitle.Text;
            _selectedMovie.DurationMin = (short)numDuration.Value;
            _selectedMovie.IsActive = chkIsActive.Checked ? "Y" : "N";

            _movieRepository.Update(_selectedMovie);

            LoadMovies();
            ClearInputs();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedMovie == null) return; 

            var result = MessageBox.Show($"Are you sure you want to delete '{_selectedMovie.Title}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                _movieRepository.Delete(_selectedMovie.MovieId);
                LoadMovies();
                ClearInputs();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvMovies.ClearSelection();
            ClearInputs();
            _selectedMovie = null;
            UpdateUIState();
        }

        private void LoadMovies()
        {
            dgvMovies.DataSource = _movieRepository.GetAll();
            dgvMovies.Columns["Showtimes"].Visible = false; 
            dgvMovies.ClearSelection();
        }

        private void ClearInputs()
        {
            txtTitle.Clear();
            numDuration.Value = 40; 
            chkIsActive.Checked = true;
            _selectedMovie = null;
        }

        private void PopulateInputs()
        {
            if (_selectedMovie != null)
            {
                txtTitle.Text = _selectedMovie.Title;
                numDuration.Value = _selectedMovie.DurationMin ?? 40;
                chkIsActive.Checked = _selectedMovie.IsActive == "Y";
            }
        }

        private void UpdateUIState()
        {
            bool isRowSelected = _selectedMovie != null;

            btnAdd.Enabled = !isRowSelected; 
            btnUpdate.Enabled = isRowSelected; 
            btnDelete.Enabled = isRowSelected; 
        }
    }
}
