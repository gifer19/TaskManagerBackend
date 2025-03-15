using TaskManager.Models;
using System.Data;
using System.Data.SqlClient;

namespace TaskManager.Data
{
    public class TaskData
    {
        private readonly string conexion;

        public TaskData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<TaskM>> ListTask()
        {
            List<TaskM> listT = new List<TaskM>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_listTask", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        listT.Add(new TaskM
                        {
                            idTask = Convert.ToInt32(reader["idTask"]),
                            titleTask = reader["titleTask"].ToString(),
                            stateTask = reader["stateTask"].ToString()
                        });
                    }
                    return listT;
                }

            }
        }

        public async Task<TaskM> GetTask(int id)
        {
            TaskM taskm = new TaskM();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_getTask", con);
                cmd.Parameters.AddWithValue("@IdTask", id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        taskm = new TaskM
                        {
                            idTask = Convert.ToInt32(reader["idTask"]),
                            titleTask = reader["titleTask"].ToString(),
                            descriptionTask = reader["descripctionTask"].ToString(),
                            stateTask = reader["titleTask"].ToString()
                        };
                    }
                    return taskm;
                }

            }
        }

        public async Task<bool> CreateTask(TaskM task)
        {
            bool result = true;

            using (var con = new SqlConnection(conexion))
            {
               
                SqlCommand cmd = new SqlCommand("sp_createTask", con);
                cmd.Parameters.AddWithValue("@TitleTask", task.titleTask);
                cmd.Parameters.AddWithValue("@DescriptionTask", task.descriptionTask);
                cmd.Parameters.AddWithValue("@State ", task.stateTask);
                cmd.CommandType = CommandType.StoredProcedure;

                try 
                {
                    await con.OpenAsync();
                   result = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch {
                    result = false;
                }

            }

            return result;
        }

        public async Task<bool> EditTask(TaskM task)
        {
            bool result = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_updateTask", con);
                cmd.Parameters.AddWithValue("@IdTask", task.idTask);
                cmd.Parameters.AddWithValue("@TitleTask", task.titleTask);
                cmd.Parameters.AddWithValue("@DescriptionTask", task.descriptionTask);
                cmd.Parameters.AddWithValue("@State ", task.stateTask);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    result = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    result = false;
                }

            }

            return result;
        }
        public async Task<bool> DeleteTask(int id)
        {
            bool result = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_deleteTask", con);
                cmd.Parameters.AddWithValue("@IdTask", id);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    result = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    result = false;
                }

            }

            return result;
        }
    }
}
