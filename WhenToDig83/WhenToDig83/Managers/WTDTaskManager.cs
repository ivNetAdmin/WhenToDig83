﻿
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhenToDig83.Core.Entities;
using WhenToDig83.Core.Enums;
using WhenToDig83.Data;

namespace WhenToDig83.Managers
{
    public class WTDTaskManager
    {
        private RepositoryAsync<WTDTask> _wtdTaskRepository;
        private RepositoryAsync<Note> _noteRepository;

        public WTDTaskManager()
        {
            _wtdTaskRepository = new RepositoryAsync<WTDTask>();
            _noteRepository = new RepositoryAsync<Note>();
        }

        public async void AddTask(string name, DateTime date, int type, string notes, int taskId)
        {
            date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            if (taskId == 0)
            {
                var wtdTask = new WTDTask { Name = name, Date = date, TypeId = type };
                await _wtdTaskRepository.Insert(wtdTask);
                if (!string.IsNullOrEmpty(notes))
                {
                    await _noteRepository.Insert(
                    new Note
                    {
                        Type = (int)NoteType.Task,
                        TypeId = wtdTask.ID,
                        Notes = notes,
                        Meta = string.Format("{0}{1}{2}", (int)NoteType.Task, type, date.ToString("ddMMMyyyy"))
                    });
                }
            }
            else
            {
                var wtdTask = await _wtdTaskRepository.Get(taskId);
                wtdTask.Name = name;
                wtdTask.TypeId = type;
                wtdTask.Date = date;                
                await _wtdTaskRepository.Update(wtdTask);

                var note = await _noteRepository.Get(predicate: x => x.Type == (int)NoteType.Task && x.TypeId == taskId);

                if (note == null)
                {
                    if (!string.IsNullOrEmpty(notes))
                    {
                        await _noteRepository.Insert(
                        new Note
                        {
                            Type = (int)NoteType.Task,
                            TypeId = taskId,
                            Notes = notes,
                            Meta = string.Format("{0}{1}{2}", (int)NoteType.Task, type, date.ToString("ddMMMyyyy"))
                        });
                    }
                }
                else
                {
                    note.Type = (int)NoteType.Task;
                    note.TypeId = taskId;
                    note.Notes = notes;
                    note.Meta = string.Format("{0}{1}{2}", (int)NoteType.Task, type, date.ToString("ddMMMyyyy"));
                    await _noteRepository.Update(note);
                }
            }
        }

        public async void DeleteTask(int taskId)
        {
            var note = await _noteRepository.Get(predicate: x => x.Type == (int)NoteType.Task && x.TypeId == taskId);
            if (note != null)
            {
                await _noteRepository.Delete(note);
            }
            var task = await _wtdTaskRepository.Get(taskId);
            await _wtdTaskRepository.Delete(task);
        }

        public async Task<List<WTDTask>> GetTasksByMonth(int month, int year)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = new DateTime(year, month + 1, 1);
            return await _wtdTaskRepository.Get(predicate: x => x.Date >= startDate && x.Date < endDate, sortOrder: "asc", orderBy: x => x.Date);
        }

        public async Task<List<WTDTask>> GetTasksByDateRange(DateTime startDate, DateTime endDate)
        {
            return await _wtdTaskRepository.Get(predicate: x => x.Date >= startDate && x.Date <= endDate, sortOrder: "asc", orderBy: x => x.Date);
        }

        public List<WTDTask> GetTasks()
        { 
            return new List<WTDTask>();
        }
    }
}
